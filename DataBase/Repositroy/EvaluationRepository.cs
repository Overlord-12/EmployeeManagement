using DataBase.Entities;
using DataBase.Repositroy.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositroy
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly BoardContext _boardContext;
        public EvaluationRepository(BoardContext boardContext)
        {
            _boardContext = boardContext;
        }

        public async Task<bool> CreateEvaluation(Evaluation evaluation)
        {
            try
            {
                _boardContext.Evaluations.Add(evaluation);
                await _boardContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                var test = ex.Message;
                return false;
                  
            }
          
        }

        public IEnumerable<Evaluation> GetEvaluations()
        {
            return _boardContext.Evaluations.Include(t=>t.User).Include(t=>t.Parameter).ToList();
        }

    }
}
