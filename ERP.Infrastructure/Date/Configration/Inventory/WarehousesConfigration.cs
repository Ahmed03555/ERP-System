using ERP.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Date.Configration.Inventory
{
    internal class WarehousesConfigration : IEntityTypeConfiguration<Warehouses>
    {
        public void Configure(EntityTypeBuilder<Warehouses> builder)
        {
            builder.ToTable("Warehouse");
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.Property(x => x.Location).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(x => x.StockItems).WithOne(x => x.Warehouse).HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.StockMovements).WithOne(x => x.Warehouses).HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
