using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productRepository = _unitOfWork.GetRepository<ERP.Domain.Entities.Inventory.Products>();

          
            var categoryExists = await _unitOfWork
                .GetRepository<ERP.Domain.Entities.Inventory.Categories>()
                .ExistsAsync(request.CategoryId, cancellationToken);

            if (!categoryExists)
                return Result<int>.Failure("Category not found.");

          
            var skuExists = await productRepository
                .Query()
                .AnyAsync(p => p.SKU == request.SKU, cancellationToken);

            if (skuExists)
                return Result<int>.Failure("A product with this SKU already exists.");

            var product = new ERP.Domain.Entities.Inventory.Products
            {
                SKU = request.SKU,
                Name = request.Name,
                CategoryId = request.CategoryId,
                UnitPrice = request.UnitPrice,
                ReorderLevel = request.ReorderLevel
            };

            await productRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(product.Id);
        }
    }
}
