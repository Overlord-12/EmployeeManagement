using DataBase.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Remote(action: "CheckName", controller: "Department", ErrorMessage = "Such a Department is already in use")]
        public string DepartmentName { get; set; }
        public int DepartmentHeadId { get; set; }
        public bool ShowPreviousEvaluations { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
