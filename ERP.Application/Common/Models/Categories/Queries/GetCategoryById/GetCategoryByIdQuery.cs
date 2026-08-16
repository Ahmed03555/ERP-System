using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(int Id) : IRequest<Result<CategoryDto>>;
}
