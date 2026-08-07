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
    internal class SuppliersConfigration : IEntityTypeConfiguration<Suppliers>
    {
        public void Configure(EntityTypeBuilder<Suppliers> builder)
        {
            builder.ToTable("Suppliers");

            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Address)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasMany(x => x.PurchaseOrders)
                .WithOne(x => x.Supplier)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
