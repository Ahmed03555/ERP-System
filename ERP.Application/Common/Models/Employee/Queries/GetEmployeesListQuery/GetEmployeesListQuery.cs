using ERP.Application.Common.Models.Employee.Queries.GetEmployeeById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Employee.Queries.GetEmployeesListQuery
{
    public record GetEmployeesListQuery : IRequest<Result<List<EmployeeDto>>>;

}
