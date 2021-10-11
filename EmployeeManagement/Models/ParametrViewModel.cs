using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class ParametrViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Coefficient { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public int? DepartmentId { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }
        public ICollection<MarkDescription> MarkDescriptions { get; set; }
        public ICollection<Selection> Selections { get; set; }
    }
}
