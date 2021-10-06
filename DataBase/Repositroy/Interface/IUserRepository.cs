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
        public  Task<bool> CreateUser(User user);
        public  Task<bool> DeleteUser(int id);
        public Task<bool> EditUser(User user);
    }
}
