using ERP.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.HR
{
    public class Departments: BaseEntity
    {
        public string Name { get; set; } = default!;

        public int? ManagerId { get; set; }
        public Employees Manager { get; set; } = default!;

        public int? EmployeesId { get; set; }

        public Employees Employees { get; set;} = default!;
    }
}
