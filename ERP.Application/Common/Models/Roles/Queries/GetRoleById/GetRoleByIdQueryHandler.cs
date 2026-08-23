using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<RoleDetailsDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork
                .GetRepository<Domain.Entities.Auth___User.Roles>()
                .Query()
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .Where(r => r.Id == request.Id)
                .Select(r => new RoleDetailsDto(
                    r.Id, r.Name,
                    r.RolePermissions.Select(rp => rp.Permission.Name).ToList()
                ))
                .FirstOrDefaultAsync(cancellationToken);

            if (role is null)
                return Result<RoleDetailsDto>.Failure("Role not found.");

            return Result<RoleDetailsDto>.Success(role);
        }
    }
}
