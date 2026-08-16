using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(int id) : IRequest<Result<bool>>;
}
