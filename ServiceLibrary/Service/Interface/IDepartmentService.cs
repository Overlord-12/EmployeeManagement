using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Service.Interface
{
    public interface IDepartmentService
    {
        public IEnumerable<Department> GetDepartments();
        public Task<bool> CreateDepartament(Department department);
        public bool DeleteDepartament(int id);
        public Task<bool> EditDepartament(Department department);
        public Department GetDepartment(int id);
    }
}
