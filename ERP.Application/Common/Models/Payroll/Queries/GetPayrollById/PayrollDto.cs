using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Payroll.Queries.GetPayrollById
{
    public record PayrollDto(
        int Id,
        int EmployeeId,
        string EmployeeName,
        int Month,
        int Year,
        decimal BaseSalary,
         decimal Deductions,
         decimal Bonuses,
         decimal NetSalary,
         string Status
        );

}
