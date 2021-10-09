using DataBase.Entities;
using EmployeeManagement.Models.Repositroy;
using EmployeeManagement.Models.Repositroy.Interface;
using EmployeeManagement.Models.Service.Interface;
using ServiceLibrary.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Service
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentService _departmentService;
        public UserService(IUserRepository userRepository, IDepartmentService departmentService)
        {
            _departmentService = departmentService;
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

        public User GetUser(User user)
        {
           return _userRepository.GetUsers().FirstOrDefault(t=>t.Login == user.Login && t.Password 
            == user.Password);
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUsers().FirstOrDefault(t=>t.Id == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }
        public IEnumerable<User> GetEmployees()
        {
            List<User> users = new List<User>();
            var _users = _userRepository.GetUsers();
            foreach(var item in _users)
            {
                foreach(var departament in _departmentService.GetDepartments())
                {
                    if (departament.DepartmentHeadId != item.Id)
                        users.Add(item);
                }
            }
            return users;
        }
    }
}
