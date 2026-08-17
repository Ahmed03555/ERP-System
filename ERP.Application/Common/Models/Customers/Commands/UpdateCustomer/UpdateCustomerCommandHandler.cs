using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerRepo = _unitOfWork.GetRepository<Domain.Entities.Customers___Sales.Customers>();

            var customerExist = await customerRepo.GetByIdAsync(request.Id, cancellationToken);

            if (customerExist is null)
                return Result<bool>.Failure("Customer not found");

            var EmailExist = await customerRepo.Query().AnyAsync(c => c.Email  == request.Email && c.Id !=request.Id, cancellationToken);

            if (EmailExist)
                return Result<bool>.Failure("A customer with this email already exists.");

            customerExist.Name = request.Name;
            customerExist.Email = request.Email;
            customerExist.PhoneNumber = request.PhoneNumber;
            customerExist.Address = request.Address;
            customerExist.CreditLimit = request.CreditLimit;

            customerRepo.UpdateAsync(customerExist);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);

        }
    }
}
