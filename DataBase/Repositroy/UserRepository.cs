using DataBase;
using DataBase.Entities;
using EmployeeManagement.Models.Repositroy.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Repositroy
{
    public class UserRepository : IUserRepository
    {
        private readonly BoardContext _boardContext;
        public UserRepository(BoardContext boardContext)
        {
            _boardContext = boardContext;
        }
        public async Task<bool> CreateUser(User user)
        {
            //User userCreate = new User
            //{
            //    RoleId = user.RoleId,
            //    StatuseId = user.StatuseId,
            //    Name = user.Name,
            //    Password = user.Password
            //};
            try
            {
                _boardContext.Add(user);
                await _boardContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                _boardContext.Users.Remove(_boardContext.Users.FirstOrDefault(t => t.Id == id));
                await _boardContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> EditUser(User user)
        {
            try
            {
                _boardContext.Users.Update(user);
                await _boardContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<User> GetUsers()
        {
            return _boardContext.Users.Include(t => t.Role).Include(t => t.Status).ToList();
        }
    }
}
