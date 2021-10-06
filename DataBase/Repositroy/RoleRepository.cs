using DataBase.Entities;
using DataBase.Repositroy.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositroy
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BoardContext _boardContext;
        public RoleRepository(BoardContext boardContext)
        {
            _boardContext = boardContext;
        }
        public IEnumerable<Role> GetRoles()
        {
            return _boardContext.Roles.ToList();
        }
    }
}
