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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly BoardContext _boardContext;
        public DepartmentRepository(BoardContext boardContext)
        {
            _boardContext = boardContext;
        }
        public async Task<bool> CreateDepartament(Department department)
        {
            try
            {
                Department dep = department;
                dep.DepartmentHead = _boardContext.Users.FirstOrDefault(t => t.Id == department.DepartmentHeadId);
                _boardContext.Departaments.Add(department);
                await _boardContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> EditDepartament(Department department)
        {
            try
            {
                _boardContext.Departaments.Update(department);
                await _boardContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> DeleteDepartament(int id)
        {
            try
            {
                var dep = _boardContext.Departaments.FirstOrDefault(t => t.Id == id);
                _boardContext.Departaments.Remove(dep);
                await _boardContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<Department> GetDepartments()
        {
            var c = _boardContext.Departaments.Include(t => t.Users).ToList();
            return _boardContext.Departaments.Include(t=>t.Users).ToList();
        }
    }
}
