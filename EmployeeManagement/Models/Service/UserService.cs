using DataBase.Entities;
using EmployeeManagement.Models.Repositroy;
using EmployeeManagement.Models.Repositroy.Interface;
using EmployeeManagement.Models.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Service
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(UserViewModel userViewModel)
        {
           return _userRepository.GetUsers().FirstOrDefault(t=>t.Name == userViewModel.Name && t.Password 
            == userViewModel.Password);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}
