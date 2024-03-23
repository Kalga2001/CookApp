using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookApp.Entity.Entity;
using CookApp.DAL.Helpers;

namespace CookApp.DAL.SeedData
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { Id = 1, Name = "John Doe", Email = "john@example.com", Password = Helper.HashPassword("password") },
                new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Password = Helper.HashPassword("password") },
                new User { Id = 3, Name = "Alice Johnson", Email = "alice@example.com", Password = Helper.HashPassword("password") }
            );
        }
    }
}
