using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)

        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryRepository = _unitOfWork.GetRepository<ERP.Domain.Entities.Inventory.Categories>();

            var nameExists = await categoryRepository.Query().AnyAsync(a => a.Name == request.Name,cancellationToken);

            if (nameExists)
                return Result<int>.Failure("A category with this name already exists.");

           if(request.ParentCategoryId is int ParentId)
            {
                var parentExists = await categoryRepository.ExistsAsync(ParentId, cancellationToken);

                if (!parentExists)
                    return Result<int>.Failure("Parent category not found.");
            }

            var category = new ERP.Domain.Entities.Inventory.Categories
            {
                Name = request.Name,
                ParentCategoryId = request.ParentCategoryId
            };
            await categoryRepository.AddAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(category.Id);
        }
    }
}

