using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid product ID.");

            RuleFor(x => x.SKU)
                .NotEmpty().WithMessage("SKU is required.")
                .MaximumLength(50).WithMessage("SKU must not exceed 50 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(150).WithMessage("Product name must not exceed 150 characters.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Invalid category ID.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

            RuleFor(x => x.ReorderLevel)
                .GreaterThanOrEqualTo(0).WithMessage("Reorder level cannot be negative.");
        }
    }
}
