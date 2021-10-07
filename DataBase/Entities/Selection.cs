using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class Selection
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string SelectionName { get; set; }
        public int ParameterId { get; set; }
        public int Mark { get; set; }

        public Department Department { get; set; }
        public Parameter Parameter { get; set; }


    }
}
