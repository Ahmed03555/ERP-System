using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Suppliers.GetSupplierById
{
    public record GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, Result<SupplierDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSupplierByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<SupplierDto>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplierRepo = _unitOfWork.GetRepository<Domain.Entities.Suppliers___Purchase.Suppliers>();

            var supliers = await supplierRepo.Query()
                .Where(s => s.Id == request.Id)
                .Select(s => new SupplierDto(
                    s.Id,
                    s.Name,
                    s.Email,
                    s.Phone,
                    s.Address)).FirstOrDefaultAsync(cancellationToken);

            if (supliers is null)
                return Result<SupplierDto>.Failure("Supplier not found.");

            return Result<SupplierDto>.Success(supliers);
        }
    }
}
