using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Warehouses.CreateWarehouse
{
    public class CreateWarehouseCommandValidator :AbstractValidator<CreateWarehouseCommand>
    {
        public CreateWarehouseCommandValidator()
        {
            RuleFor(w => w.Name).NotEmpty()
                .WithMessage("Warehouse location is required.")
                .MaximumLength(100).WithMessage("Warehouse name must not exceed 100 characters.");

            RuleFor(w => w.Location)
                .NotEmpty().WithMessage("Warehouse location is required.")
                .MaximumLength(200).WithMessage("Location must not exceed 200 characters.");
        }
    }
}
