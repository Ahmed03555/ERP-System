using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSupplierCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierRepo = _unitOfWork.GetRepository<Domain.Entities.Suppliers___Purchase.Suppliers>();

            var supplierExist = await supplierRepo.GetByIdAsync(request.Id, cancellationToken);

            if (supplierExist is null)
                return Result<bool>.Failure("Supplier not found.");

            var hasPurchaseOrders = await _unitOfWork.GetRepository<Domain.Entities.Suppliers___Purchase.PurchaseOrders>().Query()
                .AnyAsync(s => s.SupplierId == request.Id , cancellationToken);

            if (hasPurchaseOrders)
                return Result<bool>.Failure("Cannot delete a supplier that has purchase orders.");

            supplierRepo.RemoveAsync(supplierExist);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
