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
    public class StatusesService : IStatusesService
    {
        private readonly IStatusesRepository _statusesRepository;
        public StatusesService(IStatusesRepository statusesRepository)
        {
            _statusesRepository = statusesRepository;
        }
        public IEnumerable<Statuses> GetStatuses()
        {
            return _statusesRepository.GetStatuses().ToList();
        }
    }
}
