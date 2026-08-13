using ERP.Domain.Entities.Inventory;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Warehouses.CreateWarehouse
{
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateWarehouseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warhouseRepo = _unitOfWork.GetRepository<Domain.Entities.Inventory.Warehouses>();

            var nameExists = await warhouseRepo.Query().AnyAsync(a => a.Name == request.Name, cancellationToken);

            if (nameExists)
                return Result<int>.Failure("A warehouse with this name already exists.");

            var warhouse = new Domain.Entities.Inventory.Warehouses { 
            
                Name = request.Name,
                Location = request.Location,
            };
            await warhouseRepo.AddAsync(warhouse, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(warhouse.Id);
        }
    }
}
