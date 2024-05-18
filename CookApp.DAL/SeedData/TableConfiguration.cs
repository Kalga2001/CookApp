using CookApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.DAL.SeedData
{
    public class TableConfiguration: IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasData(
                new Table { Id = 1, Name = "Table 1", Capacity = 0, IsAvailable = true },
                new Table { Id = 2, Name = "Table 2", Capacity = 0, IsAvailable = true },
                new Table { Id = 3, Name = "Table 3", Capacity = 0, IsAvailable = true },
                new Table { Id = 4, Name = "Table 4", Capacity = 0, IsAvailable = true },
                new Table { Id = 5, Name = "Table 5", Capacity = 0, IsAvailable = true },
                new Table { Id = 6, Name = "Table 6", Capacity = 0, IsAvailable = true },
                new Table { Id = 7, Name = "Table 7", Capacity = 0, IsAvailable = true },
                new Table { Id = 8, Name = "Table 8", Capacity = 0, IsAvailable = true },
                new Table { Id = 9, Name = "Table 9", Capacity = 0, IsAvailable = true },
                new Table { Id = 10, Name = "Table 10", Capacity = 0, IsAvailable = true });
            
        }
    }
}
