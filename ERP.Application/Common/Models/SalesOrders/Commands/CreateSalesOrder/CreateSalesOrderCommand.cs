using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.SalesOrders.Commands.CreateSalesOrder
{
    public record SalesOrderItemDto(
        int ProductId,
        int Quantity,
        decimal UnitPrice
    );

    public record CreateSalesOrderCommand(
        int CustomerId,
        List<SalesOrderItemDto> Items
    ) : IRequest<Result<int>>;
}
