using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name, int? ParentCategoryId) : IRequest<Result<int>>;



}
