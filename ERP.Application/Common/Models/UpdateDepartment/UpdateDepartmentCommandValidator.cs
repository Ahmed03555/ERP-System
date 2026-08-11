using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.UpdateDepartment
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(d => d.Id)
                .GreaterThan(0).WithMessage("Invalid department ID.");

            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Department name is required.")
                .MaximumLength(100).WithMessage("Department name must not exceed 100 characters.");

            RuleFor(d => d.ManagerId)
                .GreaterThan(0).WithMessage("Manager ID must be a valid positive number.")
                .When(x => x.ManagerId.HasValue);

        }
    }
}
