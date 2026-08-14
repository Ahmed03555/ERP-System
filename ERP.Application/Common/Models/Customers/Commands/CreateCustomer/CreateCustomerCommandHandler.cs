using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerRepo = _unitOfWork.GetRepository<Domain.Entities.Customers___Sales.Customers>();

            var emailExist = await customerRepo.Query().AnyAsync(c => c.Email == request.Email, cancellationToken);

            if (emailExist)
                return Result<int>.Failure("A customer with this email already exists");

            var customer = new Domain.Entities.Customers___Sales.Customers
            { 
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                CreditLimit = request.CreditLimit,
            };
            await customerRepo.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(customer.Id);
        }
    }
}
