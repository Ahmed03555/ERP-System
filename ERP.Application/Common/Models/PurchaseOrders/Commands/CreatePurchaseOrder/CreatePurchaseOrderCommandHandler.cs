using ERP.Domain.Entities.Enums;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Entities.Suppliers___Purchase;
using ERP.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.PurchaseOrders.Commands.CreatePurchaseOrder
{
    public class CreatePurchaseOrderCommandHandler : IRequestHandler<CreatePurchaseOrderCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePurchaseOrderCommandHandler(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork; 
        }
        public async Task<Result<int>> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var supplierExists = await _unitOfWork.GetRepository<Domain.Entities.Suppliers___Purchase.Suppliers>().ExistsAsync(request.SupplierId, cancellationToken);

            if (!supplierExists)
                return Result<int>.Failure("Supplier not found.");

            var productRepo = _unitOfWork.GetRepository<Domain.Entities.Inventory.Products>();

            foreach(var items in request.Items)
            {
                var productExists = await productRepo.ExistsAsync(items.ProductId,cancellationToken);

                if (!productExists)
                    return Result<int>.Failure($"Product with ID {items.ProductId} not found.");
            }
            var TotalAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice);

            var purchase = new Domain.Entities.Suppliers___Purchase.PurchaseOrders 
            {
                SupplierId = request.SupplierId,
                OrderDate = DateTime.UtcNow,
                DeliveryDate = request.DeliveryDate,
                Status = PurchaseOrderStatus.Draft,
                PurchaseOrderItems = request.Items.Select(i => new PurchaseOrderItems
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()

            };

            await _unitOfWork.GetRepository<Domain.Entities.Suppliers___Purchase.PurchaseOrders>().AddAsync(purchase, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(purchase.Id);
        }
    }
}
