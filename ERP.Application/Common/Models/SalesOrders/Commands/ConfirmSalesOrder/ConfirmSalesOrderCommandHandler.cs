using ERP.Application.Common.Interfaces;
using ERP.Domain.Entities.Enums;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.SalesOrders.Commands.ConfirmSalesOrder
{
    public class ConfirmSalesOrderCommandHandler : IRequestHandler<ConfirmSalesOrderCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockService _stockService;
        private readonly ICacheService _cacheService;

        public ConfirmSalesOrderCommandHandler(IUnitOfWork unitOfWork, IStockService stockService,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _stockService = stockService;
            _cacheService = cacheService;
        }

        public async Task<Result<bool>> Handle(ConfirmSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrderRepository = _unitOfWork.GetRepository<Domain.Entities.Customers___Sales.SalesOrders>();

            var salesOrder = await salesOrderRepository
                .Query()
                .Include(so => so.SalesOrderItems)
                .FirstOrDefaultAsync(so => so.Id == request.SalesOrderId, cancellationToken);

            if (salesOrder is null)
                return Result<bool>.Failure("Sales order not found.");

            if (salesOrder.Status != SalesOrderStatus.Pending)
                return Result<bool>.Failure($"Cannot confirm a sales order that is already {salesOrder.Status}.");

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                foreach (var item in salesOrder.SalesOrderItems)
                {
                    await _stockService.DecreaseStockAsync(
                        item.ProductId,
                        request.WarehouseId,
                        item.Quantity,
                        reference: $"SalesOrder #{salesOrder.Id}",
                        cancellationToken);
                }

                salesOrder.Status = SalesOrderStatus.Confirmed;
                salesOrderRepository.UpdateAsync(salesOrder);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                await _cacheService.RemoveByPrefixAsync("salesorders:", cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<bool>.Failure(ex.Message);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            return Result<bool>.Success(true);
        }
    }
}
