using AutoMapper;
using ClosedXML.Excel;
using DataBase.Entities;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLibrary.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HeadOfDepartmentController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEvaluationService _evaluationService;
        private readonly IParametrService _parametrService;
        private readonly IMapper _mapper;
        private readonly ISelectionService _selectionService;
        private readonly IDepartmentService _departmentService;
        public HeadOfDepartmentController(IDepartmentService departmentService, ISelectionService selectionService, IMapper mapper, IParametrService parametrService,
            IEvaluationService evaluationService, IUserService userService)
        {
            _departmentService = departmentService;
            _selectionService = selectionService;
            _mapper = mapper;
            _parametrService = parametrService;
            _evaluationService = evaluationService;
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Roles = "headOfDepartament")]
        public IActionResult Index()
        {
            var id = _userService.GetById(User.Identity.Name);
            return View(_userService.GetSubordinateUsers(id));
        }
        public IActionResult AllEvaluation(int id)
        {
            return View(_evaluationService.GetEvaluationFromUser(id));
        }
        [HttpGet]
        [Authorize(Roles = "headOfDepartament")]
        public IActionResult Details(int id)
        {
            var c = _userService.GetSubordinateUsers(id);
            return View(_userService.GetSubordinateUsers(id));
        }
        [Authorize(Roles = "headOfDepartament")]
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
        [Authorize(Roles = "headOfDepartament")]
        [HttpPost]
        public async Task<IActionResult> Evaluation(EvaluationViewModel evaluationViewModel)
        {
            var eval = _mapper.Map<Evaluation>(evaluationViewModel);
            await _evaluationService.CreateEvaluation(eval);
            return RedirectToAction("Index", "HeadOfDepartment");
        }
        public IActionResult TeamLeadMarks(int id)
        {
            return View(_evaluationService.GetEvaluationFromUser(id));
        }
        [Authorize(Roles = "headOfDepartament")]
        [HttpPost]
        public IActionResult ExportToExcel(SelectionViewModel selectionViewModel)
        {
            string name = selectionViewModel.SelectionName + ".xlsx";
            var content = _selectionService.ExportSelection(_mapper.Map<Selection>(selectionViewModel));
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", name);
        }
        [Authorize(Roles = "headOfDepartament")]
        [HttpGet]
        public IActionResult Selection(IEnumerable<User> evaluations)
        {
            var departmentId = _departmentService.GetDepartments().FirstOrDefault(t => t.DepartmentHeadId == _userService.GetById(User.Identity.Name)).Id;
            ViewBag.Selection = _selectionService.GetSelectionsFromDepartment((int)departmentId);
            var selectionViewModel = new SelectionViewModel();
            selectionViewModel.Users = evaluations;
            return View(selectionViewModel);
        }
        [Authorize(Roles = "headOfDepartament")]
        [HttpPost]
        public IActionResult Selection(SelectionViewModel selectionViewModel)
        {
            var evaluation = _selectionService.GetUsers(selectionViewModel.Id);
            selectionViewModel.Users = evaluation;
            selectionViewModel.SelectionName = _selectionService.GetSelection(selectionViewModel.Id).SelectionName;
            var departmentId = _departmentService.GetDepartments().FirstOrDefault(t => t.DepartmentHeadId == _userService.GetById(User.Identity.Name)).Id;
            ViewBag.Selection = _selectionService.GetSelectionsFromDepartment((int)departmentId);
            return View(selectionViewModel);
        }


    }
}
