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
    internal class UserConfigration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(x => x.LastModifiedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.DeletedAt).IsRequired(false);
            builder.HasQueryFilter(x => !x.IsDeleted);


            builder.Property(u => u.Email)
                .HasColumnType("varchar(256)")
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.FullName)
                .HasColumnType("varchar(150)")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(ph => ph.PasswordHash)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

            builder.Property(p => p.PhoneNumber)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(A => A.IsActive)
                .HasDefaultValue(true);

            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);
                

            builder.HasMany(u => u.Notifications)
                .WithOne(n => n.Users)
                .HasForeignKey(n => n.UserId);
                

            builder.HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId);
                
        }
    }
}
