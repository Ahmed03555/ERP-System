using ERP.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Inventory
{
    public class StockItems : BaseEntity
    {
        public int ProductId { get; set; }
        public Products Product { get; set; } = default!;
        public int WarehouseId { get; set; }
        public Warehouses Warehouse { get; set; } = default!;
        public int Quantity { get; set; }
    }
}
