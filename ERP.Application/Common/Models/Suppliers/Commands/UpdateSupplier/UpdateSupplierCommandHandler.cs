using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSupplierCommandHandler(IUnitOfWork unitOfWork)
        {_unitOfWork = unitOfWork; }
        
        public async Task<Result<bool>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierRepo = _unitOfWork.GetRepository<Domain.Entities.Suppliers___Purchase.Suppliers>();

            var supplierExist = await supplierRepo.GetByIdAsync(request.Id, cancellationToken);

            if (supplierExist is null)
                return Result<bool>.Failure("Supplier not found.");

            var emailExist = await supplierRepo.Query().AnyAsync(s=> s.Email == request.Email && s.Id ==request.Id ,cancellationToken);

            if (emailExist)
                return Result<bool>.Failure("A supplier with this email already exists.");

            supplierExist.Name = request.Name;
            supplierExist.Phone = request.Phone;
            supplierExist.Email = request.Email;
            supplierExist.Address = request.Address;

            supplierRepo.UpdateAsync(supplierExist);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
