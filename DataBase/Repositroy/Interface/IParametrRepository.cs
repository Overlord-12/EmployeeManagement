using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositroy.Interface
{
   public interface IParametrRepository
    {
        public IEnumerable<Parameter> GetParameters();
        public Task<bool> Create(Parameter parametr);
        public Task<bool> Delete(int id);
        public Task<bool> Edit(Parameter parameter);
    }
}
