using DataBase.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Login is not specified")]
        [Remote(action: "CheckLogin", controller: "Admin", ErrorMessage = "Such a Login is already in use")]
        public string Login { get; set; }
        [Required(ErrorMessage = "No password specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int? SupervisorId { get; set; }
        public int? RoleId { get; set; }
        public int? StatusId { get; set; }
        public int? DepartmentId { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Status> Statuses{ get; set; }
        public IEnumerable<User> Users{ get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
   
}
