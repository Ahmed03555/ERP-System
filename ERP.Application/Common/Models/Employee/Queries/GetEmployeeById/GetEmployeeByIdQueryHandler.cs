using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Employee.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
             _unitOfWork = unitOfWork;
        }
        public async Task<Result<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employeeRepo =await _unitOfWork.GetRepository<Employees>()
                .Query()
                .Include(e => e.Departments)
                .Include(e => e.Manager)
                .Where(e => e.Id == request.Id)
                .Select(e => new EmployeeDto(
                e.Id,
                e.FullName,
                e.JobTitle,
                e.Salary,
                e.HireDate,
                e.DepartmentId,
                e.Departments != null ? e.Departments.Name : null,
                e.ManagerId,
                e.Manager != null ? e.Manager.FullName : null
            )).FirstOrDefaultAsync(cancellationToken);

            if (employeeRepo is null)
                return Result<EmployeeDto>.Failure("Employee not found.");

            return Result<EmployeeDto>.Success(employeeRepo);

        }
    }
}
