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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string employeeRoleName = "employee";
            string departamentRoleName = "headOfDepartament";
            string teamLeadRoleName = "teamRole";
            Role adminRole = new Role { Id=2, RoleName = adminRoleName };
            Role employeeRole = new Role {Id =1, RoleName = employeeRoleName };
            Role departamentRole = new Role {Id =3, RoleName = departamentRoleName };
            Role teamleadRole = new Role {Id =4, RoleName = teamLeadRoleName };

            string workStatusName = "Работает";
            string dismissedStatusName = "Уволен";
            Statuses workStatuses = new Statuses {Id =1, StatusName = workStatusName };
            Statuses dismissedStatues = new Statuses {Id =2, StatusName = dismissedStatusName };

            User employee = new User {Id =1, Name = "Employee", Password = 123, RoleId= employeeRole.Id, StatuseId = workStatuses.Id };
            User admin = new User {Id =2, Name = "Admin", Password = 123, RoleId = adminRole.Id, StatuseId = workStatuses.Id };
            User departament = new User {Id = 3, Name = "Departament", Password = 123, RoleId = departamentRole.Id, StatuseId = workStatuses.Id };
            User teamlead = new User {Id =4, Name = "TeamLead", Password = 123, RoleId = teamleadRole.Id, StatuseId = workStatuses.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] {adminRole,employeeRole,departamentRole,teamleadRole });
            modelBuilder.Entity<Statuses>().HasData(new Statuses[] { workStatuses, dismissedStatues });
            modelBuilder.Entity<User>().HasData(new User[] { employee, admin, departament, teamlead });
            base.OnModelCreating(modelBuilder);
        }
    }
}
