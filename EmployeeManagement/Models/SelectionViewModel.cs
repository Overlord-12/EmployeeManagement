using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SelectionViewModel
    {
        public IEnumerable<Parameter> Parameters { get; set; }
        public string SelectionName { get; set; }
        public string SelectionQuery { get; set; }
        public int DepartmentId { get; set; }
        public int Id { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
