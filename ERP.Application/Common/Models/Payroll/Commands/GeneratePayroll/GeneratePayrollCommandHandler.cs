using ERP.Domain.Entities.Enums;
using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Payroll.Commands.GeneratePayroll
{
    public class GeneratePayrollCommandHandler : IRequestHandler<GeneratePayrollCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GeneratePayrollCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(GeneratePayrollCommand request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.GetRepository<Employees>().GetByIdAsync(request.EmployeeId, cancellationToken);

            if (employee is null)
                return Result<int>.Failure("Employee not found.");

            var payrollRepository = _unitOfWork.GetRepository<ERP.Domain.Entities.HR.Payroll>();

            var alreadyExists = await payrollRepository.Query()
                .AnyAsync(a => a.EmployeeId ==request.EmployeeId
                && a.Month ==request.Month
                && a.Year == request.Year,cancellationToken);

            if (alreadyExists)
                return Result<int>.Failure("Payroll for this employee, month, and year already exists.");

            var absentDaysCount = await _unitOfWork.GetRepository<Attendance>
                ().Query()
                .Where(a => a.EmployeeId == request.EmployeeId
                     && a.Date.Month == request.Month
                     && a.Date.Year == request.Year
                     && a.Status == AttendanceStatus.Absent)
            .CountAsync(cancellationToken);

            var dailyRate = employee.Salary /30m;
            var deductions = dailyRate * absentDaysCount;

            var NetSalary = employee.Salary- deductions + request.Bonuses;

            var payroll = new ERP.Domain.Entities.HR.Payroll 
            {
                EmployeeId = request.EmployeeId,
                Month = request.Month,
                Year = request.Year,
                BaseSalary = employee.Salary,
                Deductions = deductions,
                Bonuses = request.Bonuses,
                NetSalary = NetSalary,
                Status = PayrollStatus.Pending
            };

            await payrollRepository.AddAsync(payroll,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(payroll.Id);
        }
    }
}
