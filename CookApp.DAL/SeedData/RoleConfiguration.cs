using CookApp.Enums;
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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { ID = 1, RoleName = RoleName.Client },
                new Role { ID = 2, RoleName = RoleName.Administrator },
                new Role { ID = 3, RoleName = RoleName.Chef }
            );
        }
    }
}
