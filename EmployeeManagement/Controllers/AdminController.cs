using AutoMapper;
using DataBase.Entities;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Service.Interface;
using Microsoft.AspNetCore.Authentication;
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
        private readonly IParametrService _parametrService;
        private readonly ISelectionService _selectionService;
        public AdminController(ISelectionService selectionServie, IParametrService parametrService, IUserService userService, IRoleService roleService,
            IStatusesService statusesService, IMapper mapper, IDepartmentService departmentService)
        {
            _selectionService = selectionServie;
            _parametrService = parametrService;
            _departmentService = departmentService;
            _statusesService = statusesService;
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View(_userService.GetUsers());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            var user = new UserViewModel();
            user.Users = _userService.GetUsers().Where(t => t.RoleId == 3 || t.RoleId == 4).ToList();
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
        public IActionResult Subordinate()
        {
            return View(_userService.GetUsers());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Details(int id)
        {
            return View(_userService.GetUser(id));
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var user = _mapper.Map<UserViewModel>(_userService.GetUser(id));
            user = CreateTransitionalUser(user);
            user.Users = _userService.GetUsers().Where(t => t.RoleId == 3 || t.RoleId == 4).ToList();
            return View(user);
        }
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckLogin(string Login)
        {
            if (_userService.GetUsers().FirstOrDefault(t => t.Login == Login) == null)
                return Json(true);
            return Json(false);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult SelectDepartment()
        {
            return View(_departmentService.GetDepartments());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Selections(int id)
        {
            SelectionViewModel selectionViewModel = new SelectionViewModel();
            selectionViewModel.Parameters = _parametrService.GetParameters().Where(t => t.DepartmentId == id);
            selectionViewModel.DepartmentId = id;
            return View(selectionViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Selections(SelectionViewModel selection, int[] Parametrs)
        {
            await _selectionService.CreateSelection(_mapper.Map<Selection>(selection), Parametrs);
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
            if (_userService.GetUser(userViewModel.Id).Login == userViewModel.Login ||
                    _userService.GetUsers().FirstOrDefault(t => t.Login == userViewModel.Login) == null)
            {
                var mainUser = _userService.GetUser(userViewModel.Id);
                var user = _mapper.Map(userViewModel, mainUser);
                await _userService.EditUser(user);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("Login", "Такой пользователь уже существует");
                userViewModel = CreateTransitionalUser(userViewModel);
                return View(userViewModel);
            }
        }
        [HttpGet]
        public IActionResult RedirectToDepartament()
        {
            return RedirectToAction("Index", "Department");
        }
        [HttpGet]
        public IActionResult RedirectToParametr()
        {
            return RedirectToAction("Index", "Parametr");
        }
        [HttpGet]
        public async Task<IActionResult> ExitAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public UserViewModel CreateTransitionalUser(UserViewModel user)
        {
            if (user.RoleId == 4)
                user.Users = _userService.GetUsers().Where(t => t.Role.RoleName == "headOfDepartament");
            else
            {
                user.Users = _userService.GetUsers().Where(t => t.RoleId == 4);
            }
            user.Statuses = _statusesService.GetStatuses();
            user.Departments = _departmentService.GetDepartments();
            user.Roles = _roleService.GetRoles();
            return user;
        }
    }
}
