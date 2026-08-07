using ERP.Domain.Entities.Suppliers___Purchase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Date.Configration.Suppliers___Purchase_Configration
{
    internal class PurchaseOrderItemsConfigrations : IEntityTypeConfiguration<PurchaseOrderItems>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItems> builder)
        {
            builder.ToTable("PurchaseOrderItems");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.PurchaseOrder).WithMany(x => x.PurchaseOrderItems)
                .IsRequired()
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.PurchaseOrderItems)
                .IsRequired()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.UnitPrice).
                IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
