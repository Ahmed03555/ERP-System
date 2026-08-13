using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Inventory.Commands.AdjustStock
{
    public enum AdjustmentType
    {
        Increase , Decrease 
    }
    public record AdjustStockCommand(
            int ProductId,
    int WarehouseId,
    int Quantity,
    AdjustmentType Type,
    string Reference
        ) : IRequest<Result<bool>>;

}
