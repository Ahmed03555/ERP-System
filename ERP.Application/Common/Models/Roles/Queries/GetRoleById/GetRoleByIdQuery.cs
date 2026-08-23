using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Queries.GetRoleById
{
    public record GetRoleByIdQuery(int Id) : IRequest<Result<RoleDetailsDto>>;
}
