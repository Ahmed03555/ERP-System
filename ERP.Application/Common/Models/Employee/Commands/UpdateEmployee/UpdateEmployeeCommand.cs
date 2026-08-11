using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Employee.Commands.UpdateEmployee
{
    public record UpdateEmployeeCommand(
                    int Id,
            string FullName,
            string JobTitle,
            decimal Salary,
            DateOnly HireDate,
            int? DepartmentId,
            int? ManagerId
        ) : IRequest<Result<bool>>;
    
    
}
