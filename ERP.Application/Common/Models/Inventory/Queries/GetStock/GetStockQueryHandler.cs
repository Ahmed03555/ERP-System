using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Inventory.Queries.GetStock
{
    public class GetStockQueryHandler : IRequestHandler<GetStockQuery, Result<StockDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStockQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<StockDto>> Handle(GetStockQuery request, CancellationToken cancellationToken)
        {
            var stock = await _unitOfWork
                .GetRepository<StockItems>()
                .Query()
                .Include(s => s.Product)
                .Include(s => s.Warehouse)
                .Where(s => s.ProductId == request.ProductId && s.WarehouseId == request.WarehouseId)
                .Select(s => new StockDto(s.ProductId, s.Product.Name, s.WarehouseId, s.Warehouse.Name, s.Quantity))
                .FirstOrDefaultAsync(cancellationToken);

            if (stock is null)
                return Result<StockDto>.Failure("No stock record found for this product/warehouse combination.");

            return Result<StockDto>.Success(stock);
        }
    }
}
