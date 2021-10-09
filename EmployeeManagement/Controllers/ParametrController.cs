using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class ParametrController:Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
