using CookApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.DAL.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { ID = 1, Name = "John Doe", Email = "john@example.com", Password = "password" },
                new User { ID = 2, Name = "Jane Smith", Email = "jane@example.com", Password = "password" },
                new User { ID = 3, Name = "Alice Johnson", Email = "alice@example.com", Password = "password" }
            );
        }
    }
}
