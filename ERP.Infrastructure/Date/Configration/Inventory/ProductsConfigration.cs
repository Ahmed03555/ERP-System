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
    internal class ProductsConfigration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
             builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(x => x.SKU)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ReorderLevel)
                .IsRequired();

            builder.HasMany(x => x.StockItems)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.StockMovements)
                .WithOne(x => x.Products)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.PurchaseOrderItems)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
