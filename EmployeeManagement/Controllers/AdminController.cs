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
        public AdminController(IUserService userService,IRoleService roleService,IStatusesService statusesService)
        {
            _statusesService = statusesService;
            _userService = userService;
            _roleService = roleService;
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
            ViewBag.Statuses = _statusesService.GetStatuses();
            ViewBag.Role = _roleService.GetRoles();
            return View(new UserViewModel());
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            User user = new User
            {
                StatuseId = userViewModel.StatuseId,
                RoleId = userViewModel.RoleId,
                Name = userViewModel.Name,
                Password = userViewModel.Password,
            };
            await _userService.CreateUser(user);
            return RedirectToAction("Index", "Admin");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUser(id);
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            ViewBag.Statuses = _statusesService.GetStatuses();
            ViewBag.Role = _roleService.GetRoles();
            var us = _userService.GetUser(id);
            UserViewModel user = new UserViewModel
            {
                Id = id,
                Name = us.Name,
                Password = us.Password,
                RoleId = us.RoleId,
                StatuseId = us.StatuseId
            };
            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
            await _userService.EditUser(userViewModel);
            return RedirectToAction("Index", "Admin");
        }
        
    }
}
