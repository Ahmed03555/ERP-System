using ERP.Domain.Entities.Customers___Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Date.Configration.Customers___Sales_Configration
{
    internal class SalesOrdersConfigration : IEntityTypeConfiguration<SalesOrders>
    {
        public void Configure(EntityTypeBuilder<SalesOrders> builder)
        {
            builder.ToTable("SalesOrders");
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.SalesOrders)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.OrderDate).HasColumnName("OrderDate").HasColumnType("datetime").IsRequired();


            builder.Property(x => x.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.Status).HasConversion<string>();

            builder.HasMany(x => x.SalesOrderItems)
                .WithOne(x => x.SalesOrder)
                .HasForeignKey(x => x.SalesOrderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
