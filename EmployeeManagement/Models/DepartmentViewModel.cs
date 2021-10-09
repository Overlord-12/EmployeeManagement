using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentHeadId { get; set; }
        public bool ShowPreviousEvaluations { get; set; }
    }
}
