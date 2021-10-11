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
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public DepartmentController(IDepartmentService departmentService, IMapper mapper, IUserService userService)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            var departments = _departmentService.GetDepartments().ToList();
            return View(departments);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult CreateDepartment()
        {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            departmentViewModel.Users = _userService.GetFreeHeadofDepartament();
            return View(departmentViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateDepartment(DepartmentViewModel departamentViewModel)
        {
            var department = _mapper.Map<Department>(departamentViewModel);
            await _departmentService.CreateDepartament(department);
            return RedirectToAction("Index", "Department");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            _departmentService.DeleteDepartament(id);
            return RedirectToAction("Index", "Department");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var departament = _departmentService.GetDepartment(id);
            return View(_mapper.Map<DepartmentViewModel>(departament));
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(DepartmentViewModel departmentViewModel)
        {
            var department = _mapper.Map<Department>(departmentViewModel);
            _departmentService.EditDepartament(department);
            return RedirectToAction("Index", "Department");
        }
    }
}
