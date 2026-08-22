using ERP.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.PurchaseOrders.Commands.Queries.GetPurchaseOrderById
{
    public record GetPurchaseOrderByIdQuery(int Id) : IRequest<Result<PurchaseOrderDto>>, ICacheableQuery
    {
        public string CacheKey => $"purchaseorders:byid:{Id}";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
}
