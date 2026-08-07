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
    internal class EmployeesConfigrations : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(e => e.HireDate)
                   .IsRequired();

            builder.Property(e => e.Salary)
                   .HasColumnType("decimal(18,2)");

            builder.Property(e => e.JobTitle)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasOne(e => e.Users)
                   .WithMany()
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasOne(e => e.Departments)
                   .WithMany()
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasOne(e => e.Manager)
                   .WithMany(e => e.Subordinates)
                   .HasForeignKey(e => e.ManagerId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired(false);

            builder.HasMany(e => e.Attendances)
                   .WithOne(a => a.Employee)
                   .HasForeignKey(a => a.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Payrolls)
                   .WithOne(p => p.Employee)
                   .HasForeignKey(p => p.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
