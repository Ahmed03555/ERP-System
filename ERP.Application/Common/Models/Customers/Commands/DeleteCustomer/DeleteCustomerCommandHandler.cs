using ERP.Application.Common.Interfaces;
using ERP.Domain.Entities.Customers___Sales;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;

        }
        public async Task<Result<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerRepo = _unitOfWork.GetRepository<Domain.Entities.Customers___Sales.Customers>();

            var customerExist = await customerRepo.GetByIdAsync(request.Id, cancellationToken);

            if (customerExist is null)
                return Result<bool>.Failure("Customer not found.");

            var hasSalesOrders = await _unitOfWork
            .GetRepository<Domain.Entities.Customers___Sales.SalesOrders>()
            .Query()
            .AnyAsync(so => so.CustomerId == request.Id, cancellationToken);

            if (hasSalesOrders)
                return Result<bool>.Failure("Cannot delete a customer that has sales orders.");

            customerRepo.RemoveAsync(customerExist);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveByPrefixAsync("customers:", cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
