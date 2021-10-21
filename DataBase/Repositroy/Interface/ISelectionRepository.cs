using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositroy.Interface
{
   public interface ISelectionRepository
    {
        public IEnumerable<Selection> Selections();
        public Task CreateSelection(Selection selection);
        public IEnumerable<Selection> GetSelectionsFromDepartment(int id);
        public IEnumerable<User> GetUsers(int id);

    }
}
