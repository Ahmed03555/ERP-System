using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Inventory.Queries.GetStock
{
    public record GetStockQuery(int ProductId, int WarehouseId) : IRequest<Result<StockDto>>;
}
