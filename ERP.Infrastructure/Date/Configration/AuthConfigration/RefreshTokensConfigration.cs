using ERP.Domain.Entities.Auth___User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Date.Configration.AuthConfigration
{
    internal class RefreshTokensConfigration : IEntityTypeConfiguration<RefreshTokens>
    {
        public void Configure(EntityTypeBuilder<RefreshTokens> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasKey(t => t.Id);
            builder.Property(x => x.LastModifiedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.DeletedAt).IsRequired(false);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(t => t.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(t => t.UserId);

            builder.Property(t => t.Token)
                .IsRequired();

            builder.Property(t => t.ExpiresOn)
                .IsRequired();

            builder.Property(t => t.CreatedOn)
                .IsRequired();


        }
    }
}
