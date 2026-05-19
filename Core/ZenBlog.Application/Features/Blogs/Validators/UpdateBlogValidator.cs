using FluentValidation;
using ZenBlog.Application.Features.Blogs.Commands;

namespace ZenBlog.Application.Features.Blogs.Validators
{
    public class UpdateBlogValidator: AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("BlogId is required.");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            
        }
    }
}
