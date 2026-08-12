using ERP.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Inventory
{
    public class Categories : BaseEntity
    {
        public string Name { get; set; } =default!;

        public int? ParentCategoryId { get; set; }
        public Categories? ParentCategory { get; set; } = default!;

        public ICollection<Categories> SubCategories { get; set; } = [];

        public ICollection<Products> Products { get; set; } = [];
    }
}

    
