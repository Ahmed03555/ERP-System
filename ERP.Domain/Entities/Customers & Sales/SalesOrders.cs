using ERP.Domain.Entities.Common;
using ERP.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Customers___Sales
{
    public class SalesOrders :BaseEntity
    {
        public int CustomerId { get; set; }
        public Customers Customer { get; set; } = default!;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public SalesOrderStatus Status { get; set; } = default!;
        public ICollection<SalesOrdersItems> SalesOrderItems { get; set; } = [];


    }
}
