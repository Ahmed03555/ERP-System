using ERP.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Queries.GetPermissionsList
{
    public record GetPermissionsListQuery : IRequest<Result<List<PermissionDto>>>, ICacheableQuery
    {
        public string CacheKey => $"permission:list";

        public TimeSpan? Expiration =>TimeSpan.FromMinutes(10);
    }
}
