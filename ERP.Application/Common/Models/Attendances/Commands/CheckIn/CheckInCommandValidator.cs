using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Attendances.Commands.CheckIn
{
    public class CheckInCommandValidator :AbstractValidator<CheckInCommand>
    {
        public CheckInCommandValidator()
        {
            RuleFor(a => a.EmployeeId)
                .GreaterThan(0).WithMessage("Invalid employee ID.");
        }
    }
}
