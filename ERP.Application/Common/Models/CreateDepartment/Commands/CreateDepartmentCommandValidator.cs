using FluentValidation;

namespace ERP.Application.Common.Models.CreateDepartment.Commands;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(100).WithMessage("Department name must not exceed 100 characters.");

        RuleFor(x => x.ManagerId)
            .GreaterThan(0).WithMessage("Manager ID must be a valid positive number.")
            .When(x => x.ManagerId.HasValue);
    }
}