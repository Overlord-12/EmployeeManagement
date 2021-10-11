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
    public class ParametrService : IParametrService
    {
        private readonly IParametrRepository _parametrRepository;
        public ParametrService(IParametrRepository parametrRepository)
        {
            _parametrRepository = parametrRepository;
        }

        public void Create(Parameter parametr)
        {
            _parametrRepository.Create(parametr);
        }

        public void Delete(int id)
        {
            _parametrRepository.Delete(id);
        }

        public void Edit(Parameter parameter)
        {
            _parametrRepository.Edit(parameter);
        }

        public Parameter GetParameter(int id)
        {
            return _parametrRepository.GetParameters().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Parameter> GetParameters()
        {
            return _parametrRepository.GetParameters();
        }
    }
}
