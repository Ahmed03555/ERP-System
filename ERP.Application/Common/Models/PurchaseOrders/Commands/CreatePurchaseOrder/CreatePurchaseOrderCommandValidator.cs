using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.PurchaseOrders.Commands.CreatePurchaseOrder
{
    public class CreatePurchaseOrderCommandValidator :AbstractValidator<CreatePurchaseOrderCommand>
    {
        public CreatePurchaseOrderCommandValidator()
        {
            RuleFor(x => x.SupplierId)
                .GreaterThan(0).WithMessage("Invalid supplier ID.");

            RuleFor(x => x.DeliveryDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Delivery date must be in the future.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Purchase order must contain at least one item.");

            RuleForEach(x => x.Items).ChildRules(items => 
            {
                items.RuleFor(i => i.ProductId)
                .GreaterThan(0).WithMessage("Invalid product ID.");

                items.RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

                items.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
            });
        }
    }
}
