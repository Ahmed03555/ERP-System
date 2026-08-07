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
    internal class PermissionsConfigrations : IEntityTypeConfiguration<Permissions>
    {
        public void Configure(EntityTypeBuilder<Permissions> builder)
        {
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LastModifiedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.DeletedAt).IsRequired(false);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Module)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(x => x.RolePermissions)
                .WithOne(x => x.Permission)
                .HasForeignKey(x => x.PermissionId);

            builder.HasQueryFilter(x => !x.IsDeleted);

        }
    }
}
