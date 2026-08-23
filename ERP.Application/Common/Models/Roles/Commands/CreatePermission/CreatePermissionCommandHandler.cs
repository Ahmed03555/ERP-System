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

namespace ERP.Application.Common.Models.Roles.Commands.CreatePermission
{

    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public CreatePermissionCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
        { _unitOfWork = unitOfWork; 
            _cacheService = cacheService;
        }

        public async Task<Result<int>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionRepository = _unitOfWork.GetRepository<Permissions>();

            var nameExists = await permissionRepository
                .Query()
                .AnyAsync(p => p.Name == request.Name, cancellationToken);

            if (nameExists)
                return Result<int>.Failure("A permission with this name already exists.");

            var permission = new Permissions
            {
                Name = request.Name,
                Module = request.Module
            };

            await permissionRepository.AddAsync(permission, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveByPrefixAsync("roles:", cancellationToken);

            return Result<int>.Success(permission.Id);
        }
    }
}
