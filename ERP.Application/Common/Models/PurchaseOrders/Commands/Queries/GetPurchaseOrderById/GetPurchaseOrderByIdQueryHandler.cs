using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.PurchaseOrders.Commands.Queries.GetPurchaseOrderById
{
    public class GetPurchaseOrderByIdQueryHandler : IRequestHandler<GetPurchaseOrderByIdQuery, Result<PurchaseOrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPurchaseOrderByIdQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<PurchaseOrderDto>> Handle(GetPurchaseOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork
                .GetRepository<Domain.Entities.Suppliers___Purchase.PurchaseOrders>()
                .Query()
                .Include(po => po.Supplier)
                .Include(po => po.PurchaseOrderItems)
                    .ThenInclude(i => i.Product)
                .Where(po => po.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)
                return Result<PurchaseOrderDto>.Failure("Purchase order not found.");

            return Result<PurchaseOrderDto>.Success(order);
        }
    }
}
