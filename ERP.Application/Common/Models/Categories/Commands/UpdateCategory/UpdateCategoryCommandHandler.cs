using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryRepo = _unitOfWork.GetRepository<Domain.Entities.Inventory.Categories>();

            var category = await categoryRepo.GetByIdAsync(request.Id,cancellationToken);

            if (category is null)
                return Result<bool>.Failure("Category not found");

            // Business Rule check Dont Repeat

            var alreadyExist = await categoryRepo.Query()
                .AnyAsync(c => c.Name == request.Name && c.Id == request.Id,cancellationToken);

            if(alreadyExist)
                return Result<bool>.Failure("A category with this name already exists.");

            if (request.ParentCategoryId == request.Id)
                return Result<bool>.Failure("A category cannot be its own parent.");

            if(request.ParentCategoryId is int parentId)
            {
                var parentExist = await categoryRepo.ExistsAsync(parentId, cancellationToken);

                if (!parentExist)
                    return Result<bool>.Failure("Parent category not found.");
            }

            category.Name = request.Name;
            category.ParentCategoryId = request.ParentCategoryId;

            categoryRepo.UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);



        }
    }
}
