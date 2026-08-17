using ERP.Application.Common.Models.Suppliers.GetSupplierById;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Suppliers.GetSupplierByList
{
    public class GetSuppliersListQueryHandler : IRequestHandler<GetSuppliersListQuery, Result<List<SupplierDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSuppliersListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<SupplierDto>>> Handle(GetSuppliersListQuery request, CancellationToken cancellationToken)
        {
            var suplies = await _unitOfWork.GetRepository<Domain.Entities.Suppliers___Purchase.Suppliers>().Query().Select(s => new SupplierDto(
                s.Id, s.Name, s.Email, s.Phone, s.Address)).ToListAsync(cancellationToken);

            return Result<List<SupplierDto>>.Success(suplies);
        }
    }
}
