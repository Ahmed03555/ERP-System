using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryRepo = _unitOfWork.GetRepository<Domain.Entities.Inventory.Categories>();

            var category = await categoryRepo.GetByIdAsync(request.id , cancellationToken);

            if (category is null)
                return Result<bool>.Failure("category not found");


            var hasSubCategories = await categoryRepo
            .Query()
            .AnyAsync(c => c.ParentCategoryId == request.id, cancellationToken);

            if (hasSubCategories)
                return Result<bool>.Failure("Cannot delete a category that has sub-categories. Delete or reassign them first.");

            var hasProducts = await _unitOfWork
                .GetRepository<Domain.Entities.Inventory.Products>()
                .Query()
                .AnyAsync(p => p.CategoryId == request.id, cancellationToken);

            if (hasProducts)
                return Result<bool>.Failure("Cannot delete a category that has products assigned to it.");

            categoryRepo.RemoveAsync(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
