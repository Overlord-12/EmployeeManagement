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
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentRepository _departamentRepository;

        public DepartmentService(IDepartmentRepository departamentRepository)
        {
            _departamentRepository = departamentRepository;
        }

        public Task<bool> CreateDepartament(Department department)
        {
            return _departamentRepository.CreateDepartament(department);
        }

        public  Task<bool> DeleteDepartament(int id)
        {
            return   _departamentRepository.DeleteDepartament(id);
        }

        public Task<bool> EditDepartament(Department department)
        {
            return _departamentRepository.EditDepartament(department);
        }

        public Department GetDepartment(int id)
        {
           return _departamentRepository.GetDepartments().FirstOrDefault(t=>t.Id == id);
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _departamentRepository.GetDepartments();
        }

    }
}
