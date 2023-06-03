using Microsoft.EntityFrameworkCore;
using P013KatmanliBlog.Core.Entities;
using System.Reflection;

namespace P013KatmanliBlog.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=P013KatmanliBlog; Trusted_Connection=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().Property(a => a.Name).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);

            modelBuilder.Entity<AppUser>().Property(a => a.Surname).HasColumnType("varchar(50)").HasMaxLength(50);

            modelBuilder.Entity<AppUser>().Property(a => a.UserName).HasColumnType("varchar(50)").HasMaxLength(50);

            modelBuilder.Entity<AppUser>().Property(a => a.Password).IsRequired().HasColumnType("nvarchar(100)").HasMaxLength(100);

            modelBuilder.Entity<AppUser>().Property(a => a.Email).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<AppUser>().Property(a => a.Phone).HasMaxLength(20);

            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = 1,
                Email = "info@P013EStore.com",
                Password = "123",
                UserName = "Admin",
                IsActive = true,
                IsAdmin = true,
                Name = "Admin",
                UserGuid = Guid.NewGuid(),
            });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

        }
    }
}
