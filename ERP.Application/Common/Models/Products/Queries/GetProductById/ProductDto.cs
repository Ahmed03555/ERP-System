using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Products.Queries.GetProductById
{
    public record ProductDto(
    int Id,
    string SKU,
    string Name,
    int CategoryId,
    string CategoryName,
    decimal UnitPrice,
    int ReorderLevel
        );
}
