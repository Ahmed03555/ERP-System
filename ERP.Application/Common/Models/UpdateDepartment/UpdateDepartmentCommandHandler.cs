using ERP.Application.Common.Interfaces;
using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        public UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork ,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }


        public async Task<Result<bool>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departrepo =  _unitOfWork.GetRepository<Departments>();

            var department = await departrepo.GetByIdAsync(request.Id, cancellationToken);

            if (department is null)
                return Result<bool>.Failure("Department not found.");

            var nameExists = await departrepo
           .Query()
           .AnyAsync(d => d.Name == request.Name && d.Id != request.Id, cancellationToken);


            if (department.ManagerId.HasValue)
            {
                var managerExists = await _unitOfWork.GetRepository<Employees>()
                    .ExistsAsync(request.ManagerId.Value, cancellationToken);

                if (!managerExists)
                    return Result<bool>.Failure("Manager not found.");
            }
            department.Name = request.Name;
            department.ManagerId = request.ManagerId;

            departrepo.UpdateAsync(department);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _cacheService.RemoveAsync("departments", cancellationToken);
           

            return Result<bool>.Success(true);
        }
    }
}
