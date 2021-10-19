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
using System.Globalization;
using System.Threading.Tasks;
using System.IO;
using ClosedXML.Excel;

namespace EmployeeManagement.Controllers
{
    public class TeamLeadController : Controller
    {
        private readonly IUserService _userService;
        private readonly IParametrService _parametrService;
        private readonly IMapper _mapper;
        private readonly IEvaluationService _evaluationService;
        private readonly ISelectionService _selectionService;
        public TeamLeadController(ISelectionService selectionService, IEvaluationService evaluation ,
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
            return RedirectToAction("Index","TeamLead");
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
            var evaluation = _selectionService.GetEvaluations(selectionViewModel.SelectionId);
            selectionViewModel.Evaluations = evaluation;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employees");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Mark";
                worksheet.Cell(currentRow, 2).Value = "Login";
                worksheet.Cell(currentRow, 3).Value = "Parametr";
                
                foreach (var user in selectionViewModel.Evaluations)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.Mark;
                    worksheet.Cell(currentRow, 2).Value = user.User.Login;
                    worksheet.Cell(currentRow, 3).Value = user.Parameter.Name;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", name);
                }
            }
        }
        [Authorize(Roles = "teamRole")]
        [HttpGet]
        public IActionResult Selection(IEnumerable<Evaluation> evaluations)
        {
            var departmentId = _userService.GetUser(_userService.GetById(User.Identity.Name)).DepartmentId;
            ViewBag.Selection = _selectionService.GetSelectionsFromDepartment((int)departmentId);
            var selectionViewModel = new SelectionViewModel();
            selectionViewModel.Evaluations = evaluations;
            return View(selectionViewModel);
        }
        [Authorize(Roles = "teamRole")]
        [HttpPost]
        public IActionResult Selection(SelectionViewModel selectionViewModel)
        {
            var evaluation = _selectionService.GetEvaluations(selectionViewModel.SelectionId);
            selectionViewModel.Evaluations = evaluation;
            selectionViewModel.SelectionName = _selectionService.GetSelection(selectionViewModel.SelectionId).SelectionName;
            var departmentId = _userService.GetUser(_userService.GetById(User.Identity.Name)).DepartmentId;
            ViewBag.Selection = _selectionService.GetSelectionsFromDepartment((int)departmentId);
            return View(selectionViewModel);
        }
        

    }
}
