using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Entities.Common;

namespace ERP.Domain.Entities.HR
{
    public class Employees : BaseEntity
    {
        public string FullName { get; set; } = default!; 

        public int? UserId { get; set; }
        public Users? Users { get; set; } = default!;

        public int? DepartmentId { get; set; }
        public Departments? Departments { get; set; } = default!;
        public DateOnly HireDate { get; set; }

        public int? ManagerId { get; set; }
        public Employees? Manager { get; set; }
        public decimal Salary { get; set; }
        public string JobTitle { get; set; } = default!;

        public ICollection<Attendance> Attendances { get; set; } = default!;

        public ICollection<Employees> Subordinates { get; set; } = default!;
        public ICollection<Payroll> Payrolls { get; set; } = default!;
    }
}