using JuanJoseHernandez.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JuanJoseHernandez.Data
{
    /// <summary>
    /// Contexto de base de datos para el sistema de gestión de citas
    /// </summary>
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<StatusEmployee> StatusEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación User -> Department (SetNull on delete)
            modelBuilder.Entity<User>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            // Relación User -> Degree (SetNull on delete)
            modelBuilder.Entity<User>()
                .HasOne(e => e.Degree)
                .WithMany(d => d.Users)
                .HasForeignKey(e => e.DegreeId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            // Relación User -> EducationLevel (SetNull on delete)
            modelBuilder.Entity<User>()
                .HasOne(e => e.EducationLevel)
                .WithMany(el => el.Users)
                .HasForeignKey(e => e.EducationLevelId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            // Relación User -> StatusEmployee (SetNull on delete)
            modelBuilder.Entity<User>()
                .HasOne(e => e.Status)
                .WithMany(s => s.Users)
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            // Índices para mejorar el rendimiento de búsquedas
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Document)
                .IsUnique();
            
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName()!.ToLower());
                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.Name.ToLower());
            }
        }
    }
}
