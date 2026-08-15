using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Commands.AssignPermissionToRole
{
    public class AssignPermissionToRoleCommandHandler : IRequestHandler<AssignPermissionToRoleCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssignPermissionToRoleCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<bool>> Handle(AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
        {
            var roleExists = await _unitOfWork
                .GetRepository<Domain.Entities.Auth___User.Roles>()
                .ExistsAsync(request.RoleId, cancellationToken);

            if (!roleExists)
                return Result<bool>.Failure("Role not found.");

            var permissionExists = await _unitOfWork
                .GetRepository<Permissions>()
                .ExistsAsync(request.PermissionId, cancellationToken);

            if (!permissionExists)
                return Result<bool>.Failure("Permission not found.");

            var rolePermissionRepository = _unitOfWork.GetRepository<RolePermissions>();

            var alreadyAssigned = await rolePermissionRepository
                .Query()
                .AnyAsync(rp => rp.RoleId == request.RoleId && rp.PermissionId == request.PermissionId, cancellationToken);

            if (alreadyAssigned)
                return Result<bool>.Failure("Role already has this permission.");

            var rolePermission = new RolePermissions
            {
                RoleId = request.RoleId,
                PermissionId = request.PermissionId
            };

            await rolePermissionRepository.AddAsync(rolePermission, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}