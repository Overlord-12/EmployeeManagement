using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Service.Interface
{
    public interface IUserService
    {
        public IQueryable<User> GetUsers();
        public User GetUser(User userViewModel);
        public User GetUser(int id);
        public Task<bool> CreateUser(User user);
        public Task<bool> DeleteUser(int id);
        public Task<bool> EditUser(User user);
        public IEnumerable<User> GetFreeHeadofDepartament();
        public IEnumerable<User> GetSubordinateUsers(int id);
        public int GetById(string login);

    }
}
