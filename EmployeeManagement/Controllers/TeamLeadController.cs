using AutoMapper;
using DataBase.Entities;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ServiceLibrary.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class TeamLeadController : Controller
    {
        private readonly IUserService _userService;
        private readonly IParametrService _parametrService;
        private readonly IMapper _mapper;
        private readonly IEvaluationService _evaluationService;
        private readonly ISelectionService _selectionService;
        public TeamLeadController(ISelectionService selectionService, IEvaluationService evaluation,
            IMapper mapper, IUserService userService, IParametrService parametrService)
        {
            _selectionService = selectionService;
            _evaluationService = evaluation;
            _mapper = mapper;
            _userService = userService;
            _parametrService = parametrService;
        }
        [Authorize(Roles = "teamRole")]
        [HttpGet]
        public IActionResult Index()
        {
            var id = _userService.GetById(User.Identity.Name);
            return View(_userService.GetSubordinateUsers(id));
        }
        [Authorize(Roles = "teamRole")]
        [HttpGet]
        public IActionResult Evaluation(int id)
        {
            var eval = new EvaluationViewModel()
            {
                UserId = id,
                AssessorId = _userService.GetUsers().FirstOrDefault(t => t.Login == User.Identity.Name).Id,
                Parameters = _parametrService.GetParameters().Where(t => t.DepartmentId == _userService.GetUser(id).DepartmentId).ToList(),
                AssessmentDate = DateTime.Now.Date
            };
            return View(eval);
        }
        [Authorize(Roles = "teamRole")]
        [HttpPost]
        public async Task<IActionResult> Evaluation(EvaluationViewModel evaluationViewModel)
        {
            var eval = _mapper.Map<Evaluation>(evaluationViewModel);
            await _evaluationService.CreateEvaluation(eval);
            return RedirectToAction("Index", "TeamLead");
        }
        [Authorize(Roles = "teamRole")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(_evaluationService.GetEvaluationFromUser(id));
        }
        [Authorize(Roles = "teamRole")]
        [HttpPost]
        public IActionResult ExportToExcel(SelectionViewModel selectionViewModel)
        {
            string name = selectionViewModel.SelectionName + ".xlsx";
            var content = _selectionService.ExportSelection(_mapper.Map<Selection>(selectionViewModel));
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", name);
        }

        
        [Authorize(Roles = "teamRole")]
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            var users = await _selectionService.ImportFromExcel(file);
            var selectionViewModel = new SelectionViewModel();
            selectionViewModel.Users = users;
            var departmentId = _userService.GetUser(_userService.GetById(User.Identity.Name)).DepartmentId;
            ViewBag.Selection = _selectionService.GetSelectionsFromDepartment((int)departmentId);
            return View("Selection", selectionViewModel);
        }
        [Authorize(Roles = "teamRole")]
        [HttpGet]
        public IActionResult Selection()
        {
            var departmentId = _userService.GetUser(_userService.GetById(User.Identity.Name)).DepartmentId;
            ViewBag.Selection = _selectionService.GetSelectionsFromDepartment((int)departmentId);
            var selectionViewModel = new SelectionViewModel();
            selectionViewModel.Users = new List<User>();
            return View(selectionViewModel);
        }
        [Authorize(Roles = "teamRole")]
        [HttpPost]
        public IActionResult Selection(SelectionViewModel selectionViewModel)
        {
            var evaluation = _selectionService.GetUsers(selectionViewModel.Id);
            selectionViewModel.Users = evaluation;
            selectionViewModel.SelectionName = _selectionService.GetSelection(selectionViewModel.Id).SelectionName;
            var departmentId = _userService.GetUser(_userService.GetById(User.Identity.Name)).DepartmentId;
            ViewBag.Selection = _selectionService.GetSelectionsFromDepartment((int)departmentId);
            return View(selectionViewModel);
        }


    }
}

