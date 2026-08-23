using ERP.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Payroll.Queries.GetPayrollById
{
    public record GetPayrollByIdQuery(int Id) :
        IRequest<Result<PayrollDto>>, ICacheableQuery
    {
        public string CacheKey => $"payroll:byid:{Id}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }

}
