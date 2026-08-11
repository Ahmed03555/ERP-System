using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Employee.CreateEmployee
{
    public record CreateEmployeeCommand(
    string FullName,
    string JobTitle,
    decimal Salary,
    DateOnly HireDate,
    int? DepartmentId,
    int? ManagerId,
    int? UserId


) : IRequest<Result<int>>;
}
