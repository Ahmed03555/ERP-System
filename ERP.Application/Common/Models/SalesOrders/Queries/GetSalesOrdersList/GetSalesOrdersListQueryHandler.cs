using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.SalesOrders.Queries.GetSalesOrdersList
{
    public class GetSalesOrdersListQueryHandler : IRequestHandler<GetSalesOrdersListQuery, Result<List<SalesOrderDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSalesOrdersListQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<List<SalesOrderDto>>> Handle(GetSalesOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork
                .GetRepository<Domain.Entities.Customers___Sales.SalesOrders>()
                .Query()
                .Include(so => so.Customer)
                .Include(so => so.SalesOrderItems)
                    .ThenInclude(i => i.Product)
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
                .ToListAsync(cancellationToken);

            return Result<List<SalesOrderDto>>.Success(orders);
        }
    }
}
