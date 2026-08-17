using ERP.Application.Common.Models.Customers.Commands.Queries.GetCustomerById;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Customers.Commands.Queries.GetCustomersList
{
    public class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, Result<List<CustomerDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomersListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<CustomerDto>>> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            var customerList = await _unitOfWork.GetRepository<Domain.Entities.Customers___Sales.Customers>().Query().Select(s => new CustomerDto(
                s.Id, s.Name, s.Email, s.PhoneNumber, s.Address, s.CreditLimit)).ToListAsync(cancellationToken);


            return Result<List<CustomerDto>>.Success(customerList);
        }
    }
}
