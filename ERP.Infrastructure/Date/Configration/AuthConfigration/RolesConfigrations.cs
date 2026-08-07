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
    internal class RolesConfigrations : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.LastModifiedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.DeletedAt).IsRequired(false);
            builder.HasQueryFilter(x => !x.IsDeleted);


            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);

            builder.HasMany(x => x.RolePermissions)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);
        }
    }
}
