using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Commands.CreatePermission
{
    public record CreatePermissionCommand(

    string Name,
    string Module) : IRequest<Result<int>>;
    
}
