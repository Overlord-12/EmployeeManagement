using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class Evaluation
    {
        public int Id { get; set; }
        public int ParameterId { get; set; }
        public int Mark { get; set; }
        public int UserId { get; set; }
        public int? AssessorId { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int AssessmentNumber { get; set; }

        public User Assessor { get; set; }
        public Parameter Parameter { get; set; }
        public User User { get; set; }

    }
}
