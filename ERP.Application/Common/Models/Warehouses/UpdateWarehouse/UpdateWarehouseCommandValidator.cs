using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Warehouses.UpdateWarehouse
{
    public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
    {

        public UpdateWarehouseCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid warehouse ID.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Warehouse name is required.")
                .MaximumLength(100).WithMessage("Warehouse name must not exceed 100 characters.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Warehouse location is required.")
                .MaximumLength(200).WithMessage("Location must not exceed 200 characters.");
        }
    }
}
