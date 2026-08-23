using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Inventory.Queries.GetStockMovements
{
    public class GetStockMovementsQueryHandler : IRequestHandler<GetStockMovementsQuery, Result<List<MovementDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStockMovementsQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<List<MovementDto>>> Handle(GetStockMovementsQuery request, CancellationToken cancellationToken)
        {
            var movements = await _unitOfWork
                .GetRepository<StockMovements>()
                .Query()
                .Include(m => m.Products)
                .Include(m => m.Warehouses)
                .OrderByDescending(m => m.Date)
                .Select(m => new MovementDto(
                    m.Id, m.ProductId, m.Products.Name, m.WarehouseId, m.Warehouses.Name,
                    m.Quantity, m.Type.ToString(), m.Reference, m.Date
                ))
                .ToListAsync(cancellationToken);

            return Result<List<MovementDto>>.Success(movements);
        }
    }
}
