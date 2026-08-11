using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public async Task<Result<bool>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeerepo = await _unitOfWork.GetRepository<Employees>().GetByIdAsync(request.Id, cancellationToken);
            
            if (employeerepo is null)
                return Result<bool>.Failure("Employee not found.");

            if (request.ManagerId == request.Id)
                return Result<bool>.Failure("An employee cannot be their own manager.");

            if(request.DepartmentId is int departmentId)
            {
                var departmentRepoExists = await _unitOfWork.GetRepository<Departments>().ExistsAsync(departmentId, cancellationToken);

                if (!departmentRepoExists)
                    return Result<bool>.Failure("Department not found.");
            }


            if(request.ManagerId is int managerId)
            {
                var managerExists = await _unitOfWork.GetRepository<Employees>().ExistsAsync(managerId, cancellationToken);

                if (!managerExists)
                    return Result<bool>.Failure("Manager not found.");
            }

            employeerepo.FullName = request.FullName;
            employeerepo.JobTitle = request.JobTitle;
            employeerepo.Salary = request.Salary;
            employeerepo.HireDate = request.HireDate;
            employeerepo.DepartmentId = request.DepartmentId;
            employeerepo.ManagerId = request.ManagerId;

           
            _unitOfWork.GetRepository<Employees>().UpdateAsync(employeerepo);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
