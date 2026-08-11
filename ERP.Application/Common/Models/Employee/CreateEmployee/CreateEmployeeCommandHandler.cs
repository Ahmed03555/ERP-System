using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Employee.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            
            if (request.DepartmentId is int departmentId)
            {
                var departmentExists = await _unitOfWork
                    .GetRepository<Departments>()
                    .ExistsAsync(departmentId, cancellationToken);

                if (!departmentExists)
                    return Result<int>.Failure("Department not found.");
            }

            
            if (request.ManagerId is int managerId)
            {
                var managerExists = await _unitOfWork
                    .GetRepository<Employees>()
                    .ExistsAsync(managerId, cancellationToken);

                if (!managerExists)
                    return Result<int>.Failure("Manager not found.");
            }

            
            if (request.UserId is int userId)
            {
                var userExists = await _unitOfWork
                    .GetRepository<Users>()
                    .ExistsAsync(userId, cancellationToken);

                if (!userExists)
                    return Result<int>.Failure("User account not found.");

                var userAlreadyLinked = await _unitOfWork
                    .GetRepository<Employees>()
                    .Query()
                    .AnyAsync(e => e.UserId == userId, cancellationToken);

                if (userAlreadyLinked)
                    return Result<int>.Failure("This user account is already linked to another employee.");
            }

       
            var employee = new Employees
            {
                FullName = request.FullName,
                JobTitle = request.JobTitle,
                Salary = request.Salary,
                HireDate = request.HireDate,
                DepartmentId = request.DepartmentId,
                ManagerId = request.ManagerId,
                UserId = request.UserId
            };

   
            var employeeRepository = _unitOfWork.GetRepository<Employees>();
            await employeeRepository.AddAsync(employee, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(employee.Id);
        }
    }
}
