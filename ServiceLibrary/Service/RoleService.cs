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
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<Role> GetRoles()
        {
            return _roleRepository.GetRoles();
        }
    }
}
