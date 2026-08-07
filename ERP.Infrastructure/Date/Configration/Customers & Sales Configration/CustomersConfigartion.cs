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
    internal class CustomersConfigartion : IEntityTypeConfiguration<Customers>
    {
        public void Configure(EntityTypeBuilder<Customers> builder)
        {
           builder.HasKey(c => c.Id);
           builder.ToTable("Customers");
            builder.Property(c => c.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(c => c.Email).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(c => c.PhoneNumber).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(c => c.Address).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(c => c.CreditLimit).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasQueryFilter(c => !c.IsDeleted); // Global query filter to exclude soft-deleted customers

            builder.HasMany(c => c.SalesOrders)
                   .WithOne(so => so.Customer)
                   .HasForeignKey(so => so.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade); // Configure cascade delete for related SalesOrders


        }
    }
}
