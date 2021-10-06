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

        public async Task<bool> CreateUser(User user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public async Task<bool> EditUser(User user)
        {
            return await _userRepository.EditUser(user);
        }

        public User GetUser(User userViewModel)
        {
           return _userRepository.GetUsers().FirstOrDefault(t=>t.Name == userViewModel.Name && t.Password 
            == userViewModel.Password);
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUsers().FirstOrDefault(t=>t.Id == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}
