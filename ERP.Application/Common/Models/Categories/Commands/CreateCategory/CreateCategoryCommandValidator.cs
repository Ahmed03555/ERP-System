using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage("Category Name is Require.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(c => c.ParentCategoryId)
                .GreaterThan(0).WithMessage("Invalid parent category ID.");

        }
    }
}
