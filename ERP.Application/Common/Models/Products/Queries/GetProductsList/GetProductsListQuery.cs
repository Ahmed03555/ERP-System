using ERP.Application.Common.Models.Products.Queries.GetProductById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Products.Queries.GetProductsList
{
    public record GetProductsListQuery : IRequest<Result<List<ProductDto>>>;
}
