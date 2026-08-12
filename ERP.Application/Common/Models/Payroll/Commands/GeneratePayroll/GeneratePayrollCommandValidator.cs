using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Payroll.Commands.GeneratePayroll
{
    public class GeneratePayrollCommandValidator :AbstractValidator<GeneratePayrollCommand>
    {
        public GeneratePayrollCommandValidator()
        {
            RuleFor(p => p.EmployeeId).GreaterThan(0)
                .WithMessage("Invalid employee ID.");

            RuleFor(p => p.Month)
                .InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");

            RuleFor(p => p.Year)
                .GreaterThanOrEqualTo(2025).WithMessage("Year cannot be in the future.");

            RuleFor(p => p.Bonuses)
                .GreaterThan(0).WithMessage("Bonuses cannot be negative.");

        }
    }
}
