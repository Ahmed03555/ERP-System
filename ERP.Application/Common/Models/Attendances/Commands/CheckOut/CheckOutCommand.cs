using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Attendances.Commands.CheckOut
{
    public record CheckOutCommand(int EmployeeId) : IRequest<Result<bool>>;
    
}
