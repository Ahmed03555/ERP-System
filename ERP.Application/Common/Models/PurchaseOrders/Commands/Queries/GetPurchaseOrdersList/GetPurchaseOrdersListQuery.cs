using ERP.Application.Common.Interfaces;
using ERP.Application.Common.Models.PurchaseOrders.Commands.Queries.GetPurchaseOrderById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.PurchaseOrders.Commands.Queries.GetPurchaseOrdersList
{
    public record GetPurchaseOrdersListQuery : IRequest<Result<List<PurchaseOrderDto>>>, ICacheableQuery
    {
        public string CacheKey => "purchaseorders:list";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
}
