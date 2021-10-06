using DataBase.Entities;
using DataBase.Repositroy.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositroy
{
    public class StatusesRepository : IStatusesRepository
    {
        private readonly BoardContext _boardContext;
        public StatusesRepository(BoardContext boardContext)
        {
            _boardContext = boardContext;
        }
        public IEnumerable<Statuses> GetStatuses()
        {
            return _boardContext.Statuses.ToList();
        }
    }
}
