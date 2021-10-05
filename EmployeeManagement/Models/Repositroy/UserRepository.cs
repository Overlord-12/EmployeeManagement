using DataBase;
using DataBase.Entities;
using EmployeeManagement.Models.Repositroy.Interface;
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
        public IEnumerable<User> GetUsers()
        {
            return _boardContext.Users.ToList();
        }
    }
}
