using FluentValidation;
using ZenBlog.Application.Features.Categories.Commands;

namespace ZenBlog.Application.Features.Categories.Validators
{
    public class UpdateCategoryValidator: AbstractValidator<UpdateCategoryCommand>
    {
      public UpdateCategoryValidator() {
            RuleFor(x => x.CategoryName).NotEmpty()
                    .WithMessage("Name is required")
                    .NotNull().WithMessage("Name cannot be null")
                    .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
                    .NotNull().WithMessage("Id cannot be null");
        }
    }
}
