using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Service.Interface
{
    public interface ISelectionService
    {
        public IEnumerable<Selection> Selections();
        public Task CreateSelection(Selection selection,int[] param);
        public IEnumerable<Selection> GetSelectionsFromDepartment(int id);
        public IEnumerable<Evaluation> GetEvaluations(int id);
        public Selection GetSelection(int id);
    }
}
