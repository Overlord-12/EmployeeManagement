using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class Department
    {

        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentHeadId { get; set; }
        public bool ShowPreviousEvaluations { get; set; }

        public User DepartmentHead { get; set; }
        public ICollection<Parameter> Parameters {get;set;}
        public ICollection<Selection> Selections { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
