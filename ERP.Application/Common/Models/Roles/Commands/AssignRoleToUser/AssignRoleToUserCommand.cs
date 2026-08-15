using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Commands.AssignRoleToUser
{
    public record AssignRoleToUserCommand(
        int UserId,
        int RoleId
        ) : IRequest<Result<bool>>;
    
    
}
