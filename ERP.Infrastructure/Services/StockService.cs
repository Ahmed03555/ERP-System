using ERP.Domain.Entities.Enums;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork _UnitOfWork;
        public StockService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
            
        }
        public async Task IncreaseStockAsync(int productId, int warehouseId, int quantity, string reference, CancellationToken cancellationToken = default)
        {
            var stockRepo = _UnitOfWork.GetRepository<StockItems>();

            var stockItemes = await stockRepo.Query().FirstOrDefaultAsync(s => s.ProductId == productId && s.WarehouseId == warehouseId, cancellationToken);
             if(stockItemes is null)
            {
                 stockItemes = new StockItems 
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    Quantity = quantity,
      
                };
                await stockRepo.AddAsync(stockItemes, cancellationToken);
            }
            else
            {
                stockItemes.Quantity += quantity;
                stockRepo.UpdateAsync(stockItemes);
            }

          
            await RecordMovementAsync(productId, warehouseId, quantity, Types.In, reference, cancellationToken);
            await _UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DecreaseStockAsync(int productId, int warehouseId, int quantity, string reference, CancellationToken cancellationToken = default)
        {
            var stockRepo = _UnitOfWork.GetRepository<StockItems>();

            var stockItem = await stockRepo
                .Query()
                .FirstOrDefaultAsync(s => s.ProductId == productId && s.WarehouseId == warehouseId, cancellationToken);

         
            if (stockItem is null || stockItem.Quantity < quantity)
                throw new InvalidOperationException(
                    $"Insufficient stock for product {productId} in warehouse {warehouseId}. " +
                    $"Available: {stockItem?.Quantity ?? 0}, Requested: {quantity}.");

            stockItem.Quantity -= quantity;  
            stockRepo.UpdateAsync(stockItem);       

           
            await RecordMovementAsync(productId, warehouseId, quantity, Types.Out, reference, cancellationToken);
            await _UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        private async Task RecordMovementAsync(int productId, int warehouseId, int quantity, Types type, string reference, CancellationToken cancellationToken)
        {
            var movment = new StockMovements 
            {
                ProductId = productId,
                WarehouseId= warehouseId,
                Quantity = quantity,
                Type = type,
                Reference = reference,
                Date = DateTime.UtcNow,
            };
            await _UnitOfWork.GetRepository<StockMovements>().AddAsync(movment,cancellationToken);
        }
    }
}
