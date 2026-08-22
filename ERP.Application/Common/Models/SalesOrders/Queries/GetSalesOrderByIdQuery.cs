using ERP.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.SalesOrders.Queries
{
    public record GetSalesOrderByIdQuery(int Id) : IRequest<Result<SalesOrderDto>>, ICacheableQuery
    {
        public string CacheKey => $"salesorders:byid:{Id}";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }

}
