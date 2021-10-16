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
    public class HeadOfDepartmentController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEvaluationService _evaluationService;
        private readonly IParametrService _parametrService;
        private readonly IMapper _mapper;
        public HeadOfDepartmentController(IMapper mapper, IParametrService parametrService, 
            IEvaluationService evaluationService, IUserService userService)
        {
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
    }
}
