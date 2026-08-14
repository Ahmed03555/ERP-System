using ERP.Domain.Entities.Enums;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Entities.Suppliers___Purchase;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.PurchaseOrders.Commands.ReceivePurchaseOrder
{
    public class ReceivePurchaseOrderCommandHandler : IRequestHandler<ReceivePurchaseOrderCommand, Result<bool>>
    {
        private readonly IStockService _stockService;
        private readonly IUnitOfWork _unitOfWork;
        public ReceivePurchaseOrderCommandHandler(IStockService stockService,IUnitOfWork unitOfWork)
        {
            _stockService = stockService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(ReceivePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrderRepo = _unitOfWork.GetRepository<Domain.Entities.Suppliers___Purchase.PurchaseOrders>();

            var purchaseOrder = await purchaseOrderRepo.Query().Include(x => x.PurchaseOrderItems).FirstOrDefaultAsync
                (p => p.Id == request.PurchaseOrderId, cancellationToken);

            if (purchaseOrder is null)
                return Result<bool>.Failure("Purchase order not found.");

            if (purchaseOrder.Status is PurchaseOrderStatus.Cancelled or PurchaseOrderStatus.Received)
                return Result<bool>.Failure($"Cannot receive a purchase order that is already {purchaseOrder.Status}.");

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try 
            {
                foreach(var item in purchaseOrder.PurchaseOrderItems)
                {
                    await _stockService.IncreaseStockAsync(
                    item.ProductId,
                    request.WarehouseId,
                    item.Quantity,
                    reference: $"PurchaseOrder #{purchaseOrder.Id}",
                    cancellationToken
                        );
                    purchaseOrder.Status = PurchaseOrderStatus.Received;
                    purchaseOrderRepo.UpdateAsync( purchaseOrder );
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }


            } catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;

            }
            return Result<bool>.Success(true);

        }
    }
}
