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
        public string SelectionQuery { get; set; }

        public Department Department { get; set; }


    }
}
