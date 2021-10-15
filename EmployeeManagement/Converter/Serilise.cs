using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DataBase.Entities;

namespace EmployeeManagement.Converter
{
    public class Serilise
    {
        public string JsonConverts<T>(IEnumerable<T> model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
