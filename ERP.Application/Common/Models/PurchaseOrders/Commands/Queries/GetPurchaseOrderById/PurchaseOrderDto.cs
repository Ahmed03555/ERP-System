using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.PurchaseOrders.Commands.Queries.GetPurchaseOrderById
{
    public record PurchaseOrderItemResponseDto(
        int ProductId,
        string ProductName,
        int Quantity,
        decimal UnitPrice
    );

    public record PurchaseOrderDto(
        int Id,
        int SupplierId,
        string SupplierName,
        DateTime OrderDate,
        DateTime DeliveryDate,
        decimal TotalAmount,
        string Status,
        List<PurchaseOrderItemResponseDto> Items
    );
}
