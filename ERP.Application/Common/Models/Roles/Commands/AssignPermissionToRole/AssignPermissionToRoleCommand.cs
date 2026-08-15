using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Commands.AssignPermissionToRole
{
    public record AssignPermissionToRoleCommand(
        int RoleId,
        int PermissionId
    ) : IRequest<Result<bool>>;
}
