using ERP.Application.Common.Interfaces;
using ERP.Application.Common.Models.Payroll.Queries.GetPayrollById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Payroll.Queries.GetPayrollByEmployee
{
    public record GetPayrollByEmployeeQuery(int EmployeeId) : IRequest<Result<List<PayrollDto>>>, ICacheableQuery
    {
        public string CacheKey => $"payroll:employee:{EmployeeId}";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
}
