using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(int Id) : IRequest<Result<ProductDto>>;
}
