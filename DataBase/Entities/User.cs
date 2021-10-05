using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Password { get; set; }
        public int? RoleId { get; set; }
        public int? StatuseId { get; set; }
        public Role Role { get; set; }
        public Statuses Statuse { get; set; }
    }
}
