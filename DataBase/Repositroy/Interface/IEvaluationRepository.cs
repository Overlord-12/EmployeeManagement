using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositroy.Interface
{
    public interface IEvaluationRepository
    {
        public IEnumerable<Evaluation> GetEvaluations();
        public Task<bool> CreateEvaluation(Evaluation evaluation);
    }
}
