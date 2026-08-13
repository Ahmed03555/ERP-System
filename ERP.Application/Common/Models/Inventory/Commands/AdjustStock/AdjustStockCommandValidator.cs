using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Inventory.Commands.AdjustStock
{
    public class AdjustStockCommandValidator:AbstractValidator<AdjustStockCommand>
    {
        public AdjustStockCommandValidator()
        {
            RuleFor(a => a.ProductId)
                .GreaterThan(0);
            RuleFor(a => a.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero");
            RuleFor(a => a.WarehouseId)
                .GreaterThan(0);

            RuleFor(a => a.Reference)
                .NotEmpty().WithMessage("Reference is required.");
        }
    }
}
