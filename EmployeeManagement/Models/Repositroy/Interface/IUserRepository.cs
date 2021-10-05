using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Repositroy.Interface
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers();
    }
}
