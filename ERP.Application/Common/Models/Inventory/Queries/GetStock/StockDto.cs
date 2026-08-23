using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Inventory.Queries.GetStock
{
    public record StockDto(int ProductId, string ProductName, int WarehouseId, string WarehouseName, int Quantity);
}
