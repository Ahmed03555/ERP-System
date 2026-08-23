using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Payroll.Queries.GetPayrollById
{
    public class GetPayrollByIdQueryHandler : IRequestHandler<GetPayrollByIdQuery, Result<PayrollDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPayrollByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<PayrollDto>> Handle(GetPayrollByIdQuery request, CancellationToken cancellationToken)
        {
            var payroll = await _unitOfWork.GetRepository<Domain.Entities.HR.Payroll>().Query()
                .Include(p => p.Employee)
                .Where(p => p.Id == request.Id)
                .Select(p => new PayrollDto(
                    p.Id,
                    p.EmployeeId,
                    p.Employee.FullName,
                    p.Month,
                    p.Year,
                    p.BaseSalary,
                    p.Deductions,
                    p.Bonuses,
                    p.NetSalary,
                    p.Status.ToString()
                    )
                ).FirstOrDefaultAsync(cancellationToken);

            if (payroll is null)
                return Result<PayrollDto>.Failure("Payroll record not found.");

            return Result<PayrollDto>.Success(payroll);
        }
    }
}
