using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.SalesOrders.Queries
{
    public class GetSalesOrderByIdQueryHandler : IRequestHandler<GetSalesOrderByIdQuery, Result<SalesOrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSalesOrderByIdQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<SalesOrderDto>> Handle(GetSalesOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork
                .GetRepository<Domain.Entities.Customers___Sales.SalesOrders>()
                .Query()
                .Include(so => so.Customer)
                .Include(so => so.SalesOrderItems)
                    .ThenInclude(i => i.Product)
                .Where(so => so.Id == request.Id)
                .Select(so => new SalesOrderDto(
                    so.Id,
                    so.CustomerId,
                    so.Customer.Name,
                    so.OrderDate,
                    so.TotalAmount,
                    so.Status.ToString(),
                    so.SalesOrderItems.Select(i => new SalesOrderItemResponseDto(
                        i.ProductId,
                        i.Product.Name,
                        i.Quantity,
                        i.UnitPrice
                    )).ToList()
                ))
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)
                return Result<SalesOrderDto>.Failure("Sales order not found.");

            return Result<SalesOrderDto>.Success(order);
        }

    }
}