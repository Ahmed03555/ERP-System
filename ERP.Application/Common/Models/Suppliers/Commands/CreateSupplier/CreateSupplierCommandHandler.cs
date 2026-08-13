using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSupplierCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierRepository = _unitOfWork.GetRepository<Domain.Entities.Suppliers___Purchase.Suppliers>();

            var emailExists = await supplierRepository
                .Query()
                .AnyAsync(s => s.Email == request.Email, cancellationToken);

            if (emailExists)
                return Result<int>.Failure("A supplier with this email already exists.");

            var supplier = new Domain.Entities.Suppliers___Purchase.Suppliers
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address
            };

            await supplierRepository.AddAsync(supplier, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(supplier.Id);
        }
    }
}
