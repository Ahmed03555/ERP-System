using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.SalesOrders.Commands.ConfirmSalesOrder
{
    public  class ConfirmSalesOrderCommandValidator : AbstractValidator<ConfirmSalesOrderCommand>
    {
        public ConfirmSalesOrderCommandValidator()
        {
            RuleFor(x => x.SalesOrderId)
                .GreaterThan(0).WithMessage("Invalid sales order ID.");

            RuleFor(x => x.WarehouseId)
                .GreaterThan(0).WithMessage("Invalid warehouse ID.");
        }
    }
}
