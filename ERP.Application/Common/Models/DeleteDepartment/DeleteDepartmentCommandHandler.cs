using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler : IRequestHandler <DeleteDepartmentCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
             _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departRepo = _unitOfWork.GetRepository<Departments>();
            var department = await departRepo.GetByIdAsync(request.Id, cancellationToken);

            if(department is null)
                return Result<bool>.Failure("Department not found.");

            var hasEmployee= await _unitOfWork.GetRepository<Employees>()
                .Query()
                .AnyAsync(d => d.DepartmentId == request.Id,cancellationToken);

            if (hasEmployee)
                return Result<bool>.Failure("Cannot delete department that has employees assigned to it.");

            departRepo.RemoveAsync(department);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);



        }
    }
}
