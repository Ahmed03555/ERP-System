using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Customers.Commands.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.GetRepository<Domain.Entities.Customers___Sales.Customers>().Query().Where(c => c.Id==request.Id)
                .Select(s => new CustomerDto(s.Id, s.Name, s.Email, s.PhoneNumber, s.Address, s.CreditLimit))
                .FirstOrDefaultAsync(cancellationToken);

            if (customer is null)
                return Result<CustomerDto>.Failure("Customer not found.");

            return Result<CustomerDto>.Success(customer);
        }
    }
}
