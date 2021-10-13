using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Service.Interface
{
    public interface IEvaluationService
    {
        public IEnumerable<Evaluation> GetEvaluations();
        public IEnumerable<Evaluation> GetEvaluationFromUser(int id);
        public Task CreateEvaluation(Evaluation evaluation);
    }
}
