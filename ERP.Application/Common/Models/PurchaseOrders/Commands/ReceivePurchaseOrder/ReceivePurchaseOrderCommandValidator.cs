using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.PurchaseOrders.Commands.ReceivePurchaseOrder
{
    public class ReceivePurchaseOrderCommandValidator : AbstractValidator<ReceivePurchaseOrderCommand>
    {
        public ReceivePurchaseOrderCommandValidator()
        {
            RuleFor(x => x.WarehouseId)
                .GreaterThan(0).WithMessage("WarehouseId not valied");

            RuleFor(x => x.PurchaseOrderId)
                .GreaterThan(0).WithMessage("PurchaseOrderId not valied");
        }
    }
}
