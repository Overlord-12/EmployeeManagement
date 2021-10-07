using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class MarkDescription
    {
        public int Id { get; set; }
        public string Specification { get; set; }
        public int Mark { get; set; }
        public Parameter Parametr { get; set; }
        public int? ParametrId { get; set; }
    }
}
