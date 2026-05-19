using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using ZenBlog.Application.Features.Blogs.Commands;

namespace ZenBlog.Application.Features.Blogs.Validators
{
    public class CreateBlogValidator: AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x=>x.UserId).NotEmpty().WithMessage("UserId is required.");
        }
    }
}
