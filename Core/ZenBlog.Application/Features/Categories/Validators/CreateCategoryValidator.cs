

using FluentValidation;
using FluentValidation.Validators;
using System.Security.Cryptography.X509Certificates;
using ZenBlog.Application.Features.Categories.Commands;

namespace ZenBlog.Application.Features.Categories.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator() { 
            RuleFor(x =>x.CategoryName).NotEmpty()
                .WithMessage("Name is required")
                .NotNull().WithMessage("Name cannot be null")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

        }
    }
}
