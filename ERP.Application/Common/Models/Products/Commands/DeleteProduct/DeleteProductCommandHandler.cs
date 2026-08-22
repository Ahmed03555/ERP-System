using ERP.Application.Common.Interfaces;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }
        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var ProductRepo = _unitOfWork.GetRepository<Domain.Entities.Inventory.Products>();

            var productExist = await ProductRepo.GetByIdAsync(request.Id, cancellationToken);

            if (productExist is null)
                return Result<bool>.Failure("Product not found");

            var StockItemsExist = await _unitOfWork.GetRepository<Domain.Entities.Inventory.StockItems>()
                .Query()
                .AnyAsync(s => s.ProductId == request.Id, cancellationToken);

            if (StockItemsExist)
                return Result<bool>.Failure("Cannot delete a product that has stock records. Remove stock first.");

            ProductRepo.RemoveAsync(productExist);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveByPrefixAsync("products:",cancellationToken);


            return Result<bool>.Success(true);
        }
    }
}
