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
    internal class PayrollConfigration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("Payrolls");
            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasOne(p => p.Employee)
                   .WithMany(e => e.Payrolls)
                   .HasForeignKey(p => p.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.Month).IsRequired();

            builder.Property(p => p.Year).IsRequired();
            builder.Property(p => p.BaseSalary).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Deductions).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Bonuses).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.NetSalary).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Status).HasConversion<string>().IsRequired();




        }
    }
}
