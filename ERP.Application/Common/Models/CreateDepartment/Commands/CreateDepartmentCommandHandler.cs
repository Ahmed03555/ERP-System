using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.CreateDepartment.Commands
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentRepository = _unitOfWork.GetRepository<Departments>();
            if (request.ManagerId.HasValue)
            {
                var managerExists = await _unitOfWork.GetRepository<Departments>()
                    .ExistsAsync(request.ManagerId.Value);
                if (!managerExists)
                {
                    return Result<int>.Failure("Manager not found.");
                }
            }
            var nameExists = await departmentRepository
           .Query()
           .AnyAsync(d => d.Name == request.Name, cancellationToken);

            if (nameExists)
                return Result<int>.Failure("A department with this name already exists.");

            var department = new Departments
            {
                Name = request.Name,
                ManagerId = request.ManagerId
            };

        
            await departmentRepository.AddAsync(department, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(department.Id);


        }        
    }
}
