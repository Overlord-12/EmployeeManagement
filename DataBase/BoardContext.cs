using DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class BoardContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Selection> Selections { get; set; }
        public virtual DbSet<Parameter> Parametrs { get; set; }
        public virtual DbSet<MarkDescription> MarksDescriptions { get; set; }
        public virtual DbSet<Evaluation> Evaluations { get; set; }
        public virtual DbSet<Department> Departaments { get; set; }
        public BoardContext(DbContextOptions<BoardContext> options)
         : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentName).IsRequired();

                entity.HasOne(d => d.DepartmentHead)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.DepartmentHeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Departments_Users");
            });

            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.Property(e => e.AssessmentDate).HasColumnType("date");

                entity.HasOne(d => d.Assessor)
                    .WithMany(p => p.EvaluationAssessors)
                    .HasForeignKey(d => d.AssessorId)
                    .HasConstraintName("FK_Evaluations_Users1");

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.Evaluations)
                    .HasForeignKey(d => d.ParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evaluations_Parameters");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EvaluationUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evaluations_Users");
            });

            modelBuilder.Entity<MarkDescription>(entity =>
            {
                entity.Property(e => e.Specification)
                    .IsRequired()
                    .HasColumnName("MarkDescription");

                entity.HasOne(d => d.Parametr)
                    .WithMany(p => p.MarkDescriptions)
                    .HasForeignKey(d => d.ParametrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MarkDescriptions_Parameters");
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName).IsRequired();
            });

            modelBuilder.Entity<Selection>(entity =>
            {
                entity.Property(e => e.SelectionName).IsRequired();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Selections)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Selections_Departments");

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.Selections)
                    .HasForeignKey(d => d.ParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Selections_Parameters");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusName).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {

                entity.Property(e => e.Login).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Departments");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Statuses");
            });
            string adminRoleName = "admin";
            string employeeRoleName = "employee";
            string departamentRoleName = "headOfDepartament";
            string teamLeadRoleName = "teamRole";
            Role adminRole = new Role { Id = 2, RoleName = adminRoleName };
            Role employeeRole = new Role { Id = 1, RoleName = employeeRoleName };
            Role departamentRole = new Role { Id = 3, RoleName = departamentRoleName };
            Role teamleadRole = new Role { Id = 4, RoleName = teamLeadRoleName };

            string workStatusName = "Работает";
            string dismissedStatusName = "Уволен";
            Status workStatuses = new Status { Id = 1, StatusName = workStatusName };
            Status dismissedStatues = new Status { Id = 2, StatusName = dismissedStatusName };

            User employee = new User { Id = 1, Login = "Employee", Password = "123", RoleId = employeeRole.Id, StatusId = workStatuses.Id };
            User admin = new User { Id = 2, Login = "Admin", Password = "123", RoleId = adminRole.Id, StatusId = workStatuses.Id };
            User departament = new User { Id = 3, Login = "Departament", Password = "123", RoleId = departamentRole.Id, StatusId = workStatuses.Id };
            User teamlead = new User { Id = 4, Login = "TeamLead", Password = "123", RoleId = teamleadRole.Id, StatusId = workStatuses.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, employeeRole, departamentRole, teamleadRole });
            modelBuilder.Entity<Status>().HasData(new Status[] { workStatuses, dismissedStatues });
            modelBuilder.Entity<User>().HasData(new User[] { employee, admin, departament, teamlead });
            base.OnModelCreating(modelBuilder);

        }
    }
}
