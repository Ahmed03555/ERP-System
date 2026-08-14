using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.SalesOrders.Commands.CreateSalesOrder
{
    public class CreateSalesOrderCommandValidator : AbstractValidator<CreateSalesOrderCommand>
    {
        public CreateSalesOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Invalid customer ID.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Sales order must contain at least one item.");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId)
                    .GreaterThan(0).WithMessage("Invalid product ID.");

                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

                item.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
            });
        }

    }
}
