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


namespace ERP.Application.Common.Models.Roles.Commands.CreateRole
{

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public CreateRoleCommandHandler(IUnitOfWork unitOfWork,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }
        public async Task<Result<int>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleRepo = _unitOfWork.GetRepository<Domain.Entities.Auth___User.Roles>();

            var nameExist = await roleRepo.Query().AnyAsync(r => r.Name == request.Name, cancellationToken);

            if (nameExist)
                return Result<int>.Failure("A role with this name already exists.");

            var role = new Domain.Entities.Auth___User.Roles
            {
                Name = request.Name,
            };

            await roleRepo.AddAsync(role, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveByPrefixAsync("roles:", cancellationToken);

            return Result<int>.Success(role.Id);
        }
    }
}
