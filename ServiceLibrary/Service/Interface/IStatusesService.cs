using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Service.Interface
{
    public interface IStatusesService
    {
        public IEnumerable<Status> GetStatuses();
    }
}
