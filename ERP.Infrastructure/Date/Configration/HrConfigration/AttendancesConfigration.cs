using ERP.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Date.Configration.HrConfigration
{
    internal class AttendancesConfigration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasQueryFilter(a => !a.IsDeleted);
            builder.ToTable("Attendances");
            builder.HasQueryFilter(a => !a.IsDeleted);
            builder.HasOne(a => a.Employee)
                   .WithMany(e => e.Attendances)
                   .HasForeignKey(a => a.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(a => a.Date)
                   .IsRequired();
            builder.Property(a => a.CheckIn)
       .IsRequired();

            builder.Property(a => a.CheckOut)
                   .IsRequired(false);

            builder.Property(a => a.Status).HasConversion<string>().IsRequired();

        }
    }
}
