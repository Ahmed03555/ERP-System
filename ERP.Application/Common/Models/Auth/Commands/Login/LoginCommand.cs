using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;

    public record LoginResponse(string AccessToken, string RefreshToken);

}
