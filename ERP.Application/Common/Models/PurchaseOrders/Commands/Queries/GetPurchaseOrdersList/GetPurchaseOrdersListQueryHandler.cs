using ERP.Application.Common.Models.PurchaseOrders.Commands.Queries.GetPurchaseOrderById;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.PurchaseOrders.Commands.Queries.GetPurchaseOrdersList
{
    public class GetPurchaseOrdersListQueryHandler : IRequestHandler<GetPurchaseOrdersListQuery, Result<List<PurchaseOrderDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPurchaseOrdersListQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<List<PurchaseOrderDto>>> Handle(GetPurchaseOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork
                .GetRepository<Domain.Entities.Suppliers___Purchase.PurchaseOrders>()
                .Query()
                .Include(po => po.Supplier)
                .Include(po => po.PurchaseOrderItems)
                    .ThenInclude(i => i.Product)
                .Select(po => new PurchaseOrderDto(
                    po.Id,
                    po.SupplierId,
                    po.Supplier.Name,
                    po.OrderDate,
                    po.DeliveryDate,
                    po.TotalAmount,
                    po.Status.ToString(),
                    po.PurchaseOrderItems.Select(i => new PurchaseOrderItemResponseDto(
                        i.ProductId,
                        i.Product.Name,
                        i.Quantity,
                        i.UnitPrice
                    )).ToList()
                ))
                .ToListAsync(cancellationToken);

            return Result<List<PurchaseOrderDto>>.Success(orders);
        }
    }
}
