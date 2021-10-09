using AutoMapper;
using DataBase.Entities;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLibrary.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IStatusesService _statusesService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public AdminController(IUserService userService,IRoleService roleService,
            IStatusesService statusesService, IMapper mapper, IDepartmentService departmentService)
        {
            _departmentService = departmentService;
            _statusesService = statusesService;
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles ="admin")]
        public IActionResult Index()
        {
            return View(_userService.GetUsers());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            var user = new UserViewModel();
            user.Users = _userService.GetUsers().Where(t => t.Role.RoleName == "headOfDepartament");
            user.Statuses = _statusesService.GetStatuses();
            user.Departments = _departmentService.GetDepartments();
            user.Roles = _roleService.GetRoles();
            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);
            await _userService.CreateUser(user);
            return RedirectToAction("Index", "Admin");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
          
            var c = await _userService.DeleteUser(id);
            if (c == true)
                return RedirectToAction("Index", "Admin");
            else
                return View("Нельзя удалить данного пользователя, так как он является главой департамента");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var user = _mapper.Map<UserViewModel>(_userService.GetUser(id));
            user.Users = _userService.GetUsers().Where(t => t.Role.RoleName == "headOfDepartament");
            user.Statuses = _statusesService.GetStatuses();
            user.Departments = _departmentService.GetDepartments();
            user.Roles = _roleService.GetRoles();
            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);
            await _userService.EditUser(user);
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult RedirectToDepartament()
        {
            return RedirectToAction("Index","Department");
        }   
        public IActionResult Exit()
        { 
            return RedirectToAction("Login", "Account");
        }
    }
}
