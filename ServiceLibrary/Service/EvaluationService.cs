using DataBase.Entities;
using DataBase.Repositroy.Interface;
using ServiceLibrary.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Service
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IEvaluationRepository _evaluationRepository; 
        public EvaluationService(IEvaluationRepository evaluationRepository)
        {
            _evaluationRepository = evaluationRepository;
        }
        public Task CreateEvaluation(Evaluation evaluation)
        {
           return _evaluationRepository.CreateEvaluation(evaluation);
        } 

        public IEnumerable<Evaluation> GetEvaluationFromUser(int id)
        {
           return _evaluationRepository.GetEvaluations().Where(t => t.UserId == id).ToList();
        }

        public IEnumerable<Evaluation> GetEvaluations()
        {
            return _evaluationRepository.GetEvaluations();
        }
    }
}
