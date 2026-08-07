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
    internal class DepartmentsConfigration : IEntityTypeConfiguration<Departments>
    {
        public void Configure(EntityTypeBuilder<Departments> builder)
        {
            builder.HasKey(d => d.Id);
            builder.HasQueryFilter(d => !d.IsDeleted);
            builder.Property(d => d.Name)
                .IsRequired()
                .HasColumnType("nvarchar(100)");
            builder.HasOne(d => d.Manager)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(d => d.Employees)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(d => d.EmployeesId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
