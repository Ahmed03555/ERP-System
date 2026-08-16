using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Warehouses.CreateWarehouse.DeleteWarehouse
{
    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWarehouseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehousesRepo = _unitOfWork.GetRepository<Domain.Entities.Inventory.Warehouses>();

            var warehousesExist = await warehousesRepo.GetByIdAsync(request.Id, cancellationToken);

            if (warehousesExist is null)
                return Result<bool>.Failure("Warehouse not found.");

            var stockItemsExist = await _unitOfWork.GetRepository<Domain.Entities.Inventory.StockItems>()
                .Query()
                .AnyAsync(s => s.WarehouseId == request.Id,cancellationToken);

            if (stockItemsExist)
                return Result<bool>.Failure("Cannot delete a warehouse that has stock records.");

            warehousesRepo.RemoveAsync(warehousesExist);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
