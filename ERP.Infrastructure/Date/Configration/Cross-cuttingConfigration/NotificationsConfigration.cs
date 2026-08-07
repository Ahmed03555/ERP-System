using ERP.Domain.Entities.Cross_cutting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Date.Configration.Cross_cuttingConfigration
{
    internal class NotificationsConfigration : IEntityTypeConfiguration<Notifications>
    {
        public void Configure(EntityTypeBuilder<Notifications> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsRead).IsRequired();
            builder.Property(x => x.Body).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Title).HasColumnType("nvarchar(500)");
            builder.HasOne(x => x.Users)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
