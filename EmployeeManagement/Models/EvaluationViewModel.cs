using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EvaluationViewModel
    {
        public int Id { get; set; }
        public int ParameterId { get; set; }
        public int Mark { get; set; }
        public int UserId { get; set; }
        public int? AssessorId { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int AssessmentNumber { get; set; }
        public string MarkDescription { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }
    }
}
