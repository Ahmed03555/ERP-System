using ERP.Application.Common.Models.Categories.Queries.GetCategoryById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Categories.Queries.GetCategoriesList
{
    public record GetCategoriesListQuery : IRequest<Result<List<CategoryDto>>>;

}
