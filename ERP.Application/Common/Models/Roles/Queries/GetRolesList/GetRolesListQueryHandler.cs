using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Queries.GetRolesList
{
    public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, Result<List<RoleDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRolesListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<RoleDto>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.GetRepository<Domain.Entities.Auth___User.Roles>().Query()
            .Select(r => new RoleDto(r.Id, r.Name))
            .ToListAsync(cancellationToken);

            return Result<List<RoleDto>>.Success(roles);
        }
    }
}
