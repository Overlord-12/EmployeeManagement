using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class Parameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Coefficient { get; set; }
        public Department Department { get; set; }
        public int? DepartmentId { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }
    }
}
