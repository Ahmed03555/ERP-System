using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Commands.CreateRole
{
    public record CreateRoleCommand(string Name) : IRequest<Result<int>>;

}
