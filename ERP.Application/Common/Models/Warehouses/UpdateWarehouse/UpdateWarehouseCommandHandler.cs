using ERP.Application.Common.Interfaces;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Warehouses.UpdateWarehouse
{
    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public UpdateWarehouseCommandHandler(IUnitOfWork unitOfWork,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }
        public async Task<Result<bool>> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouseRepo = _unitOfWork.GetRepository<Domain.Entities.Inventory.Warehouses>();
            var warehouseExist = await warehouseRepo
                .GetByIdAsync(request.Id, cancellationToken);

            if (warehouseExist is null)
                return Result<bool>.Failure("warehouse not found");

            var nameExist = await warehouseRepo
                .Query()
                .AnyAsync(w => w.Name == request.Name && w.Id != request.Id,cancellationToken);

            if(nameExist)
                return Result<bool>.Failure("A warehouse with this name already exists.");

            warehouseExist.Name = request.Name;
            warehouseExist.Location = request.Location;

            warehouseRepo.UpdateAsync(warehouseExist);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveByPrefixAsync("warehouses:", cancellationToken);

            return Result<bool>.Success(true);

           
        }
    }
}
