using ERP.Domain.Entities.Customers___Sales;
using ERP.Domain.Entities.Enums;
using ERP.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.SalesOrders.Commands.CreateSalesOrder
{
    public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSalesOrderCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var customerExists = await _unitOfWork
                .GetRepository<Domain.Entities.Customers___Sales.Customers>()
                .ExistsAsync(request.CustomerId, cancellationToken);

            if (!customerExists)
                return Result<int>.Failure("Customer not found.");

            var productRepository = _unitOfWork.GetRepository<Domain.Entities.Inventory.Products>();

            foreach (var item in request.Items)
            {
                var productExists = await productRepository.ExistsAsync(item.ProductId, cancellationToken);

                if (!productExists)
                    return Result<int>.Failure($"Product with ID {item.ProductId} not found.");
            }

            var totalAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice);

            var salesOrder = new Domain.Entities.Customers___Sales.SalesOrders
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                Status = SalesOrderStatus.Pending,
                SalesOrderItems = request.Items.Select(i => new SalesOrdersItems
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            await _unitOfWork.GetRepository<Domain.Entities.Customers___Sales.SalesOrders>().AddAsync(salesOrder, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(salesOrder.Id);
        }
    }
}
