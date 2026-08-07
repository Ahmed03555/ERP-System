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
    internal class AuditLogsConfigrations : IEntityTypeConfiguration<AuditLogs>
    {
        public void Configure(EntityTypeBuilder<AuditLogs> builder)
        {

            builder.ToTable("AuditLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Entity)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.EntityId)
                   .IsRequired();

            builder.Property(x => x.Action).HasConversion<string>()
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.OldValues)
                   .HasColumnType("nvarchar(max)");

            builder.Property(x => x.NewValues)
                   .HasColumnType("nvarchar(max)");

            builder.HasOne(x => x.Users)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
