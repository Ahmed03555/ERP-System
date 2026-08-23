using ERP.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Queries.GetRolesList
{
    public record GetRolesListQuery : IRequest<Result<List<RoleDto>>>, ICacheableQuery
    {
        public string CacheKey => "roles:list";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
}
