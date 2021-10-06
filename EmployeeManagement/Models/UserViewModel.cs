using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class UserViewModel:User
    {
        [Required(ErrorMessage = "Email is not specified")]
        public string Name { get; set; }
        [Required(ErrorMessage = "No password specified")]
        [DataType(DataType.Password)]
        public int Password { get; set; }
    }
}
