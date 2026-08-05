using ERP.Domain.Entities.Common;
using ERP.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Inventory
{
    public class StockMovements : BaseEntity
    {
        public int ProductId { get; set; }
        public Products Products { get; set; } = default!;

        public int WarehouseId { get; set; }
        public Warehouses Warehouses { get; set; } = default!;

        public int Quantity { get; set; }

        public Types Type { get; set; }

        public string Reference { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}
