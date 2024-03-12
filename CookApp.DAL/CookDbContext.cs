using CookApp.DAL.EntityConfiguration;
using CookApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CookApp.Context
{
    public class CookDbContext : DbContext
    {
        public CookDbContext(DbContextOptions<CookDbContext> options) : base(options)
        {
        }

        public CookDbContext()
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());


            base.OnModelCreating(builder);
        }

    }
}
