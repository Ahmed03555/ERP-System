using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Enums
{
    public enum SalesOrderStatus
    {
        Pending = 1,
        Confirmed,
        Shipped,
        Delivered,
        Cancelled
    }
}
