﻿using AutoMapper;
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

namespace EmployeeManagement.Controllers
{
    public class TeamLeadController : Controller
    {
        private readonly IUserService _userService;
        private readonly IParametrService _parametrService;
        private readonly IMapper _mapper;
        private readonly IEvaluationService _evaluationService;
        public TeamLeadController(IEvaluationService evaluation ,IMapper mapper, IUserService userService, IParametrService parametrService)
        {
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
            var c = _evaluationService.GetEvaluationFromUser(id);
            return View(_evaluationService.GetEvaluationFromUser(id));
        }

    }
}
