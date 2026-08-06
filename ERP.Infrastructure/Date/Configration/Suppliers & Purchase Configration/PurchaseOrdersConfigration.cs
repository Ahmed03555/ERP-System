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
    internal class PurchaseOrdersConfigration : IEntityTypeConfiguration<PurchaseOrders>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrders> builder)
        {
            builder.ToTable("PurchaseOrders");
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.Supplier)
                .WithMany(x => x.PurchaseOrders)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(P => P.OrderDate)
                .IsRequired();

            builder.Property(P => P.DeliveryDate)
                .IsRequired();

            builder.Property(P => P.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(P => P.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.HasMany(x => x.PurchaseOrderItems)
                .WithOne(x => x.PurchaseOrder)
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
