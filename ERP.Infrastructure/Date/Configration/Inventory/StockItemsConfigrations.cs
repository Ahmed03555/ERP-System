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

    internal class StockItemsConfigrations : IEntityTypeConfiguration<StockItems>
    {
        public void Configure(EntityTypeBuilder<StockItems> builder)
        {
            builder.ToTable("StockItem");
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.StockItems)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Warehouse)
                .WithMany(x => x.StockItems)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Quantity).IsRequired();
        }
    }
}
