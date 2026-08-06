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
    internal class StockMovementsConfigration : IEntityTypeConfiguration<StockMovements>
    {
        public void Configure(EntityTypeBuilder<StockMovements> builder)
        {
            builder.ToTable("StockMovement");
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.HasOne(x => x.Products).WithMany(x => x.StockMovements).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Warehouses).WithMany(x => x.StockMovements).HasForeignKey(x => x.WarehouseId);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Type).HasConversion<string>().IsRequired();
            builder.Property(x => x.Reference).HasMaxLength(100);
            builder.Property(x => x.Date).IsRequired();

        }
    }
}
