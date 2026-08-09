using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator :AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator() {
             RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
        }
    }
}
