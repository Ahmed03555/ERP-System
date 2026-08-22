using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.SalesOrders.Queries
{
    public record SalesOrderItemResponseDto(
        int ProductId,
        string ProductName,
        int Quantity,
        decimal UnitPrice
    );

    public record SalesOrderDto(
        int Id,
        int CustomerId,
        string CustomerName,
        DateTime OrderDate,
        decimal TotalAmount,
        string Status,
        List<SalesOrderItemResponseDto> Items
    );
}
