using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(
        int Id ,
        string Name,
          int? ParentCategoryId
        ) : IRequest<Result<bool>>;

}
