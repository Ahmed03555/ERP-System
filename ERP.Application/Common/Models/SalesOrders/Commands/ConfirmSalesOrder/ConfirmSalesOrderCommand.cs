using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.SalesOrders.Commands.ConfirmSalesOrder
{
    public record ConfirmSalesOrderCommand(
        int SalesOrderId,
        int WarehouseId
        ) : IRequest<Result<bool>>;

}
