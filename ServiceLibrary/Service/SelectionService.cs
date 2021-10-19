using DataBase.Entities;
using DataBase.Repositroy.Interface;
using ServiceLibrary.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Service
{
    public class SelectionService : ISelectionService
    {
        private readonly ISelectionRepository _selectionRepository;
        public SelectionService(ISelectionRepository selectionRepository)
        {
            _selectionRepository = selectionRepository;
        }
        public Task CreateSelection(Selection selection, int[] param)
        {
            string selectionString = $"SELECT TOP(5) ev.*, us.Login, par.Coefficient, par.Name  \n" +
                                    $"FROM Evaluations ev \n" +
                                    $"inner join Users us on us.Id = ev.UserId \n" +
                                    $"inner join Parametrs par on par.Id = ev.ParameterId \n";
            for (int i = 0; i < param.Length; i++)
            {
                if (i == 0)
                    selectionString += $"where ev.ParameterId = {param[i]}";
                else if (i == param.Length - 1)
                    selectionString += $" or ev.ParameterId = {param[i]} \n and DATEDIFF(MONTH,ev.AssessmentDate,GETDATE()) < 3";
                else
                    selectionString += $" or ev.ParameterId = {param[i]}";
            }
            selectionString += "order by ev.Mark * par.Coefficient DESC";
            selection.SelectionQuery = selectionString;
            return _selectionRepository.CreateSelection(selection);
        }

        public IEnumerable<Evaluation> GetEvaluations(int id)
        {
            return _selectionRepository.GetEvaluations(id);
        }

        public Selection GetSelection(int id)
        {
            return _selectionRepository.Selections().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Selection> GetSelectionsFromDepartment(int id)
        {
            return _selectionRepository.GetSelectionsFromDepartment(id);
        }

        public IEnumerable<Selection> Selections()
        {
            return _selectionRepository.Selections();
        }
    }
}
