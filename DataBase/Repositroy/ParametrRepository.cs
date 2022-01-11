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
    public class ParametrRepository : IParametrRepository
    {

        private readonly BoardContext _boardContext;

        public ParametrRepository(BoardContext boardContext)
        {
            _boardContext = boardContext;
        }

        public async Task<bool> Create(Parameter parametr)
        {
            try
            {
                await _boardContext.AddAsync(parametr);
                _boardContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                _boardContext.Remove(_boardContext.Parametrs.FirstOrDefault(t => t.Id == id));
                await _boardContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> Edit(Parameter parameter)
        {
            try
            {
                _boardContext.Update(parameter);
                await _boardContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Parameter> GetParameters()
        {
            return _boardContext.Parametrs.Include(t=>t.Department).ToArray();
        }
    }
}
