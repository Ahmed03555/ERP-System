using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.PurchaseOrders.Commands.CreatePurchaseOrder
{
    public record PurchaseOrderItemDto(
        int ProductId,
        int Quantity,
        decimal UnitPrice
        );


    public record CreatePurchaseOrderCommand(
        int SupplierId,
        DateTime DeliveryDate,
        List<PurchaseOrderItemDto> Items
        ) : IRequest<Result<int>>;
}
