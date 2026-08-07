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
    internal class SalesOrdersItemsConfigration : IEntityTypeConfiguration<SalesOrdersItems>
    {
        public void Configure(EntityTypeBuilder<SalesOrdersItems> builder)
        {
            builder.ToTable("SalesOrdersItems");
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(x => x.Quantity).IsRequired().HasColumnType("int");
            builder.Property(x => x.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            

            builder.HasOne(x => x.SalesOrder)
                .WithMany(x => x.SalesOrderItems)
                .HasForeignKey(x => x.SalesOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                   .WithMany(x => x.SalesOrderItems)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
