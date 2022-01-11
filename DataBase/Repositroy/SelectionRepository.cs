using DataBase.Entities;
using DataBase.Repositroy.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositroy
{
    public class SelectionRepository : ISelectionRepository
    {
        private readonly BoardContext _boardContext;

        public SelectionRepository(BoardContext boardContext)
        {
            _boardContext = boardContext;
        }

        public async Task CreateSelection(Selection baseSelection, int[] param)
        {
            try
            {
                Selection selection = CreateSelectionFromParametr(baseSelection,param);
                _boardContext.Add(selection);
                await _boardContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var exeption = ex.Message;
            }
           
        }

        public IEnumerable<User> GetUsers(int id)
        {
            try
            {
                var selection = _boardContext.Selections.FirstOrDefault(t => t.Id == id);
                var users = _boardContext.Users.FromSqlRaw(selection.SelectionQuery).Include(t=>t.Department).Include(t=>t.Role).ToList();
                return users;
            }
            catch(Exception)
            {
                IEnumerable<User> evaluation = new List<User>();
                return evaluation;
            }
           
        }

        public IEnumerable<Selection> GetSelectionsFromDepartment(int id)
        {
            return _boardContext.Selections.Where(t=>t.DepartmentId == id).ToList();
        }

        public IEnumerable<Selection> GetSelections()
        {
            return _boardContext.Selections.ToList();
        }

        // Creating selection by selected user parametrs
        private Selection CreateSelectionFromParametr(Selection baseSelection, int[] param)
        {
            string selectionString = $"SELECT TOP(5) us.*, ev1.total \n" +
                                     $"from Users us \n" +
                                    $"inner join(select ev.UserId, sum(ev.Mark* par.Coefficient) as total \n" +
                                    $"from Evaluations ev \n" +
                                    $"inner join Parametrs par on par.Id = ParameterId \n";
            for (int i = 0; i < param.Length; i++)
            {
                if (i == 0)
                    selectionString += $"where ev.ParameterId = {param[i]}";
                else if (i == param.Length - 1)
                    selectionString += $" or ev.ParameterId = {param[i]} \n and DATEDIFF(MONTH,ev.AssessmentDate,GETDATE()) < 3";
                else
                    selectionString += $" or ev.ParameterId = {param[i]}";
            }
            selectionString += $"group by ev.UserId) as ev1 on us.Id = ev1.UserId \n" +
                               $"order by ev1.total DESC \n";
            baseSelection.SelectionQuery = selectionString;
            return baseSelection;
        }
    }
}
