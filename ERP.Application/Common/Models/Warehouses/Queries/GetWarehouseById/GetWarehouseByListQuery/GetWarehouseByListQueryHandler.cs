using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Warehouses.Queries.GetWarehouseById.GetWarehouseByListQuery
{
    public class GetWarehouseByListQueryHandler : IRequestHandler<GetWarehouseByListQuery, Result<List<WarehouseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetWarehouseByListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<WarehouseDto>>> Handle(GetWarehouseByListQuery request, CancellationToken cancellationToken)
        {
            var warehouseRepo = _unitOfWork.GetRepository<Domain.Entities.Inventory.Warehouses>();

            var warehouse = await warehouseRepo.Query()
                
                .Select(w => new WarehouseDto(
                    w.Id,
                    w.Name,
                    w.Location

                )).ToListAsync(cancellationToken);
            return Result<List<WarehouseDto>>.Success(warehouse);
        }
    }
}
