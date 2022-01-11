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
            try
            {
                if (user.RoleId == 3 || user.RoleId == 2)
                    user.SupervisorId = null;
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
            catch (Exception ex)
            {
                var massage = ex.Message;
                return false;
            }
        }

        public async Task<bool> EditUser(User user)
        {
            try
            {
                if (user.RoleId == 3 || user.RoleId == 2)
                    user.SupervisorId = null;
                _boardContext.Update(user);
                await _boardContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                var c = ex.Message;
                return false;
            }
        }

        public IQueryable<User> GetUsers()
        {
            return _boardContext.Users.Include(t => t.Role).Include(t => t.Status).Include(t => t.Department).
                Include(t=>t.EvaluationUsers).Include(t=>t.EvaluationAssessors);
        }

        public User Login(User model)
        {
            try
            {

                var boardContext = _boardContext.Users.FirstOrDefault(t => t.Login == model.Login);
                if (boardContext.Login != model.Login) throw new Exception();
                var password = _boardContext.Users.FirstOrDefault(t => t.Login == model.Login).Password;
                User user = _boardContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Login == model.Login && password == model.Password);
                return user;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
