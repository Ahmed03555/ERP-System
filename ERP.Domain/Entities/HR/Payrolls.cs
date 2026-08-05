using ERP.Domain.Entities.Common;
using ERP.Domain.Entities.Enums;

namespace ERP.Domain.Entities.HR
{
    public class Payroll : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employees Employee { get; set; } = null!;

        public int Month { get; set; }

        public int Year { get; set; }

        public decimal BaseSalary { get; set; }

        public decimal Deductions { get; set; }

        public decimal Bonuses { get; set; }

        public decimal NetSalary { get; set; }

        public PayrollStatus Status { get; set; }
    }
}