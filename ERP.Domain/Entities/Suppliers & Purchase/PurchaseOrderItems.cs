using ERP.Domain.Entities.Common;
using ERP.Domain.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Suppliers___Purchase
{
    public class PurchaseOrderItems :BaseEntity
    {
        public int PurchaseOrderId { get; set; }
        public PurchaseOrders PurchaseOrder { get; set; } = default!;
        public int ProductId { get; set; }
        public Products Product { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
