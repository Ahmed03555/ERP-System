using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Employee.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(150).WithMessage("Full name must not exceed 150 characters.");

            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("Job title is required.")
                .MaximumLength(100).WithMessage("Job title must not exceed 100 characters.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than zero.");

            RuleFor(x => x.HireDate)
                .NotEmpty().WithMessage("Hire date is required.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("Hire date cannot be in the future.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("Invalid department ID.")
                .When(x => x.DepartmentId.HasValue);

            RuleFor(x => x.ManagerId)
                .GreaterThan(0).WithMessage("Invalid manager ID.")
                .When(x => x.ManagerId.HasValue);

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Invalid user ID.")
                .When(x => x.UserId.HasValue);
        }
    }
}
