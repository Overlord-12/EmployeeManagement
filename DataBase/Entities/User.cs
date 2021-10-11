using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public int? DepartmentId { get; set; }
        public int SupervisorId { get; set; }

        public Department Department { get; set; }
        public Role Role { get; set; }
        public Status Status { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<Evaluation> EvaluationAssessors { get; set; }
        public ICollection<Evaluation> EvaluationUsers { get; set; }
    }
}
