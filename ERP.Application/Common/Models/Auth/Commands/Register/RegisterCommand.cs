using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Auth.Commands.Register
{
    public record RegisterCommand(
        string FullName,
        string Email,
        string Password,
        string? PhoneNumber
    ) : IRequest<Result<int>>;


}
