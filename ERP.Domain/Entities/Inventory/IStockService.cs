using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Inventory
{
    public interface IStockService
    {
        Task IncreaseStockAsync(int productId, int warehouseId, int quantity, string reference, CancellationToken cancellationToken = default);
        Task DecreaseStockAsync(int productId, int warehouseId, int quantity, string reference, CancellationToken cancellationToken = default);
    }
}
