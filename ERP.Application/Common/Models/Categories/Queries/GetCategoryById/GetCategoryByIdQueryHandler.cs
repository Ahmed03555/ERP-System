using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.GetRepository<Domain.Entities.Inventory.Categories>().Query()
                 .Include(c => c.ParentCategory)
                 .Where(c => c.Id == request.Id)
                 .Select(c => new CategoryDto(
                     c.Id
                     , c.Name
                     , c.ParentCategoryId,
                     c.ParentCategory != null ? c.ParentCategory.Name : null))
                 .FirstOrDefaultAsync(cancellationToken);

            if (category is null)
                return Result<CategoryDto>.Failure("Category not found.");

            return Result<CategoryDto>.Success(category);

        }
    }
}
