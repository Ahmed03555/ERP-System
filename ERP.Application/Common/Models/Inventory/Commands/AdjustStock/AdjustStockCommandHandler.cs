using ERP.Domain.Entities.Inventory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Inventory.Commands.AdjustStock
{
    public class AdjustStockCommandHandler : IRequestHandler<AdjustStockCommand, Result<bool>>
    {
        private readonly IStockService _stockService;
        public AdjustStockCommandHandler(IStockService stockService)
        {
            _stockService = stockService;
        }
        public async Task<Result<bool>> Handle(AdjustStockCommand request, CancellationToken cancellationToken)
        {

            try
            {
                if(request.Type == AdjustmentType.Increase)
                {
                    await _stockService.IncreaseStockAsync(request.ProductId, request.WarehouseId, request.Quantity, request.Reference, cancellationToken);

                }
                else
                {
                    await _stockService.DecreaseStockAsync(request.ProductId, request.WarehouseId, request.Quantity, request.Reference, cancellationToken);

                }
                return Result<bool>.Success(true);
            }
            catch (InvalidOperationException ex) {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
