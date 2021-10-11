using AutoMapper;
using DataBase.Entities;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLibrary.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class ParametrController:Controller
    {
        private readonly IParametrService _parametrService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public ParametrController(IParametrService parametrService, IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _parametrService = parametrService;
        }
        [Authorize(Roles ="admin")]
        [HttpGet]
        public IActionResult Index()
        {
            return View(_parametrService.GetParameters());
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _parametrService.Delete(id);
            return RedirectToAction("Index","Parametr");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ParametrViewModel parametrViewModel = new ParametrViewModel();
            parametrViewModel.Departments = _departmentService.GetDepartments();
            return View(parametrViewModel);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create(ParametrViewModel parametrViewModel)
        {
            var parametr = _mapper.Map<Parameter>(parametrViewModel);
            _parametrService.Create(parametr);
            return RedirectToAction("Index", "Parametr");
        }
    }
}
