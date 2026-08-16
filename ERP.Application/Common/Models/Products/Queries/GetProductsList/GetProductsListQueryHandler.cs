using ERP.Application.Common.Models.Products.Queries.GetProductById;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Products.Queries.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, Result<List<ProductDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<ProductDto>>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var productRepo = _unitOfWork.GetRepository<Domain.Entities.Inventory.Products>();

            var productExist = await productRepo.Query().Include(p => p.Category)
                .Select(src => new ProductDto(
                    src.Id,
                    src.SKU,
                    src.Name,
                    src.CategoryId,
                    src.Category.Name,
                    src.UnitPrice,
                    src.ReorderLevel))
                .ToListAsync(cancellationToken);

            return Result<List<ProductDto>>.Success(productExist);
        }
    }
}
