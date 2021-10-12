using EmployeeManagement.Models.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class TeamLeadController : Controller
    {
        private readonly IUserService _userService;
        public TeamLeadController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "teamRole")]
        [HttpGet]
        public IActionResult Index()
        {
            var id = _userService.GetById(User.Identity.Name);
            return View(_userService.GetSubordinateUsers(id));
        }
    }
}
