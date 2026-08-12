using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Payroll.Commands.GeneratePayroll
{
    public record GeneratePayrollCommand(
        int EmployeeId,
        int Month,
        int Year,
        decimal Bonuses
        ) : IRequest<Result<int>>;

}
