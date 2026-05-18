using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Features.Users.Results;

namespace ZenBlog.Application.Features.Users.Commands
{
    public class CreateUserCommand: IRequest<BaseResult<CreateUserResult>>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

    }
}
