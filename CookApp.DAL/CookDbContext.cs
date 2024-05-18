using CookApp.DAL.SeedData;
using CookApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CookApp.DAL
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
        public DbSet<Product> Product { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new TableConfiguration());

            base.OnModelCreating(builder);
        }

    }
}
