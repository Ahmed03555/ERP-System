using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(e =>e.Id).GreaterThan(0)
                .WithMessage("Invalid employee ID.");

            RuleFor(e => e.FullName).NotEmpty()
                .WithMessage("Full name is required.")
                .MaximumLength(150).WithMessage("Full name must not exceed 150 characters.");

            RuleFor(e => e.JobTitle).NotEmpty()
                .WithMessage("Job title is required.")
                .MaximumLength(100).WithMessage("Job title must not exceed 100 characters.");

            RuleFor(e => e.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than zero.");

            RuleFor(e => e.DepartmentId)
                .GreaterThan(0).WithMessage("Invalid department ID")
                .When(x => x.DepartmentId.HasValue);


            RuleFor(e =>e.ManagerId)
                .GreaterThan(0).WithMessage("Invalid manager ID.")
                .When(x => x.ManagerId.HasValue);

            RuleFor(e => e.HireDate)
                .NotEmpty().WithMessage("Hire date is required.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Hire date cannot be in the future.");


        }
    }
}
