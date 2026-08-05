using ERP.Domain.Entities.Common;
using ERP.Domain.Entities.Enums;

namespace ERP.Domain.Entities.HR
{
    public class Attendance : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employees Employee { get; set; } = null!;

        public DateOnly Date { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public AttendanceStatus Status { get; set; }
    }
}