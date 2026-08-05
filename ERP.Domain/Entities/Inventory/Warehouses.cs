using ERP.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Inventory
{
    public class Warehouses : BaseEntity
    {
       public string Location { get; set; } = default!;
        public string Name { get; set; } = default!;
       public ICollection<StockItems> StockItems { get; set; } = [];
        public ICollection<StockMovements> StockMovements { get; set; } = [];
    }
}
