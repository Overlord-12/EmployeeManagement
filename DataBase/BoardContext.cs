using DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class BoardContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public BoardContext(DbContextOptions<BoardContext> options)
         : base(options)
        {
        }
    }
}
