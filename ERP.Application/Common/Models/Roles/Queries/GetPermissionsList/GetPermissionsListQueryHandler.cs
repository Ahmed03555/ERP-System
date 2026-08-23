using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Queries.GetPermissionsList
{
    public class GetPermissionsListQueryHandler : IRequestHandler<GetPermissionsListQuery, Result<List<PermissionDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionsListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<PermissionDto>>> Handle(GetPermissionsListQuery request, CancellationToken cancellationToken)
        {
            var pernission = await _unitOfWork.GetRepository<Permissions>().Query()
            .Select(p => new PermissionDto(p.Id, p.Name, p.Module))
            .ToListAsync(cancellationToken);

            return Result<List<PermissionDto>>.Success(pernission);
        }
    }
}
