using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositroy.Interface
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> GetDepartments();
        public Task<bool> CreateDepartament(Department department);
        public Task<bool> EditDepartament(Department department);
        public Task<bool> DeleteDepartament(int id);
    }
}
