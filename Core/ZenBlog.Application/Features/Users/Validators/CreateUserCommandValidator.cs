using FluentValidation;
using ZenBlog.Application.Features.Users.Commands;

namespace ZenBlog.Application.Features.Users.Validators
{
    public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .Matches("^[a-zA-Z0-9_]*$")
                .WithMessage("Username can only contain letters, numbers, and underscores");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Password must contain an uppercase letter")
                .Matches("[0-9]").WithMessage("Password must contain a number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain a special character");
        }
    }
}
