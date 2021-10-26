using DataBase.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Service.Interface
{
    public interface ISelectionService
    {
        public IEnumerable<Selection> Selections();
        public Task CreateSelection(Selection selection,int[] param);
        public byte[] ExportSelection(Selection selection);
        public  Task<List<User>> ImportFromExcel(IFormFile file);
        public IEnumerable<Selection> GetSelectionsFromDepartment(int id);
        public IEnumerable<User> GetUsers(int id);
        public Selection GetSelection(int id);
    }
}
