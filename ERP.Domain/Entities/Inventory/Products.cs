using ERP.Domain.Entities.Common;
using ERP.Domain.Entities.Customers___Sales;
using ERP.Domain.Entities.Suppliers___Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Inventory
{
    public class Products : BaseEntity
    {
        public string SKU { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int CategoryId { get; set; }
        public Categories Category { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public int ReorderLevel { get; set; }

        public ICollection<StockItems> StockItems { get; set; } =  [];
        public ICollection<StockMovements> StockMovements { get; set; } = [];

        public ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; } = [];
        public ICollection<SalesOrdersItems> SalesOrderItems { get; set; } = [];
    }
}
