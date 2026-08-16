using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            
            var productRepo = _unitOfWork.GetRepository<Domain.Entities.Inventory.Products>();

            var productExist = await productRepo.Query().Include(p => p.Category)
                .Where(p => p.Id == request.Id)
                .Select(src => new ProductDto(
                    src.Id,
                    src.SKU,
                    src.Name,
                    src.CategoryId,
                    src.Category.Name,
                    src.UnitPrice,
                    src.ReorderLevel
                    )).FirstOrDefaultAsync(cancellationToken);

            if (productExist is null)
                return Result<ProductDto>.Failure("Product not Found");

            return Result<ProductDto>.Success(productExist);


            
        }
    }
}
