using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Service.Interface
{
   public interface IParametrService
    {
        public IEnumerable<Parameter> GetParameters();
        public void Create(Parameter parametr);
        public void Delete(int id);
        public void Edit(Parameter parameter);
        public Parameter GetParameter(int id);
    }
}
