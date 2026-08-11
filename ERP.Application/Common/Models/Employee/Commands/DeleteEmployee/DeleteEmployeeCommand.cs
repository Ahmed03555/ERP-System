using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Employee.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(int Id) : IRequest<Result<bool>>;
}
