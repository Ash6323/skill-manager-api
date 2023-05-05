using EmployeeSkillManager.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkillManager.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>()
                        .HasKey(a => a.Id)
                        .HasName("PrimaryKey_AdminId");

            modelBuilder.Entity<Employee>()
                        .HasKey(e => e.Id)
                        .HasName("PrimaryKey_EmployeeId");

            modelBuilder.Entity<Skill>()
                        .HasKey(s => s.Id)
                        .HasName("PrimaryKey_SkillId");

            modelBuilder.Entity<ProfileImage>()
                        .HasOne(p => p.User)
                        .WithOne(p => p.ProfileImages);

            modelBuilder.Entity<Employee>()
                        .HasMany(e => e.Skills)
                        .WithMany(e => e.Employees)
                        .UsingEntity<EmployeeSkill>(
                            l => l.HasOne<Skill>(e => e.Skill).WithMany(e => e.EmployeeSkills),
                            r => r.HasOne<Employee>(e => e.Employee).WithMany(e => e.EmployeeSkills));

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
    }
}
