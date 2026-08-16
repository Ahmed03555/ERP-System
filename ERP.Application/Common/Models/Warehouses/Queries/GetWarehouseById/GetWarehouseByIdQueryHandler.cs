using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Warehouses.Queries.GetWarehouseById
{
    public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseById, Result<WarehouseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWarehouseByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<WarehouseDto>> Handle(GetWarehouseById request, CancellationToken cancellationToken)
        {
            var warehouseRepo =  _unitOfWork.GetRepository<Domain.Entities.Inventory.Warehouses>();

            var warehouse = await warehouseRepo.Query()
                .Where(w => w.Id == request.Id)
                .Select(src => new WarehouseDto(
                    src.Id,
                    src.Name,
                    src.Location))
                .FirstOrDefaultAsync(cancellationToken);

            if (warehouse is null)
                return Result<WarehouseDto>.Failure("Warehouse not found.");

            return Result<WarehouseDto>.Success(warehouse);


        }
    }
}
