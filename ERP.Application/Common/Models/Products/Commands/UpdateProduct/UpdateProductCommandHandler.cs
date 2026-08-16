using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productRepo = _unitOfWork.GetRepository<Domain.Entities.Inventory.Products>();

            var productExist = await productRepo.GetByIdAsync(request.Id, cancellationToken);

            if (productExist is null)
                return Result<bool>.Failure("Product not found.");

            var categoryExist = await _unitOfWork.GetRepository<Domain.Entities.Inventory.Categories>()
                .ExistsAsync(request.Id, cancellationToken);

            if (!categoryExist)
                return Result<bool>.Failure("Category not found.");

            var skuExist = await productRepo.Query().AnyAsync(s => s.SKU == request.SKU,cancellationToken);

            if (skuExist)
                return Result<bool>.Failure("A product with this SKU already exists.");

            productExist.SKU= request.SKU;
            productExist.Name = request.Name;
            productExist.CategoryId = request.CategoryId;
            productExist.UnitPrice = request.UnitPrice;
            productExist.ReorderLevel = request.ReorderLevel;

             productRepo.UpdateAsync(productExist);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
