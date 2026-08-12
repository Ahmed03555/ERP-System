using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Products.Commands.CreateProduct
{
    public record CreateProductCommand(
    string SKU,
    string Name,
    int CategoryId,
    decimal UnitPrice,
    int ReorderLevel
) : IRequest<Result<int>>;
}
