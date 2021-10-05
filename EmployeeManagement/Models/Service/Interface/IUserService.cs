using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Service.Interface
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();
        public User GetUser(UserViewModel userViewModel);
    }
}
