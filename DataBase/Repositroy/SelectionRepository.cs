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
        public async Task CreateSelection(Selection selection)
        {
            try
            {
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

        public IEnumerable<Selection> Selections()
        {
            return _boardContext.Selections.ToList();
        }
    }
}
