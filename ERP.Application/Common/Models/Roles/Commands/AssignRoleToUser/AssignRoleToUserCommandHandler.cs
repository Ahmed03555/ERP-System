using ERP.Application.Common.Interfaces;
using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Commands.AssignRoleToUser
{
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        public AssignRoleToUserCommandHandler(IUnitOfWork unitOfWork,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }
        public async Task<Result<bool>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var UserRepo = _unitOfWork.GetRepository<Users>();
            var userExist = await UserRepo.ExistsAsync(request.UserId, cancellationToken);

            if (!userExist)
                return Result<bool>.Failure("User not found.");

            var RoleExist = await _unitOfWork.GetRepository<Domain.Entities.Auth___User.Roles>().ExistsAsync(request.RoleId, cancellationToken);

            if (!RoleExist)
                return Result<bool>.Failure("Role not Found.");

            var userRoleRepository = _unitOfWork.GetRepository<UserRoles>();

            var alreadyAssigned = await userRoleRepository.Query().AnyAsync(ur =>
            ur.UserId == request.UserId && ur.RoleId == request.RoleId, cancellationToken);

            if (alreadyAssigned)
                return Result<bool>.Failure("User already has this role.");

            var roleUser = new UserRoles
            { 
                UserId = request.UserId,
                RoleId = request.RoleId
            };

            await userRoleRepository.AddAsync(roleUser, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveByPrefixAsync("roles:", cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
