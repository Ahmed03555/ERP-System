using ERP.Application.Common.Models.Payroll.Queries.GetPayrollById;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Payroll.Queries.GetPayrollByEmployee
{
    public class GetPayrollByEmployeeQueryHandler : IRequestHandler<GetPayrollByEmployeeQuery, Result<List<PayrollDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPayrollByEmployeeQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<List<PayrollDto>>> Handle(GetPayrollByEmployeeQuery request, CancellationToken cancellationToken)
        {
            var records = await _unitOfWork
                .GetRepository<Domain.Entities.HR.Payroll>()
                .Query()
                .Include(p => p.Employee)
                .Where(p => p.EmployeeId == request.EmployeeId)
                .OrderByDescending(p => p.Year).ThenByDescending(p => p.Month)
                .Select(p => new PayrollDto(
                    p.Id, p.EmployeeId, p.Employee.FullName, p.Month, p.Year,
                    p.BaseSalary, p.Deductions, p.Bonuses, p.NetSalary, p.Status.ToString()
                ))
                .ToListAsync(cancellationToken);

            return Result<List<PayrollDto>>.Success(records);
        }
    }
}
