using ERP.Domain.Entities.Common;
using ERP.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Suppliers___Purchase
{
    public class PurchaseOrders :BaseEntity
    {
        public int SupplierId { get; set; }
        public Suppliers Supplier { get; set; } = default!;
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        public PurchaseOrderStatus Status { get; set; } = default!;

        public ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; } = [];

    }
}
