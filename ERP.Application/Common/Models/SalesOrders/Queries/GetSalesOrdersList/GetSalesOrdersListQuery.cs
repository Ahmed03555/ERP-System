using ERP.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.SalesOrders.Queries.GetSalesOrdersList
{

    public record GetSalesOrdersListQuery : IRequest<Result<List<SalesOrderDto>>>, ICacheableQuery
    {
        public string CacheKey => "salesorders:list";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
}
