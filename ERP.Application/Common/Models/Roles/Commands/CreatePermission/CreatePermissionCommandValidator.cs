using ERP.Application.Common.Models.Roles.Commands.CreatePermission;
using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Roles.Commands.CreatePermission
{
    public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
    {
        public CreatePermissionCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Permission name is required.")
                .MaximumLength(100).WithMessage("Permission name must not exceed 100 characters.");

            RuleFor(x => x.Module)
                .NotEmpty().WithMessage("Module name is required.")
                .MaximumLength(50).WithMessage("Module name must not exceed 50 characters.");
        }
    }
}