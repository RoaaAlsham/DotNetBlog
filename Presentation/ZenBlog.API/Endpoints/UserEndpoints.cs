using ZenBlog.Application.Features.Users.Commands;
using MediatR;
namespace ZenBlog.API.Endpoints
{
    public static class UserEndpoints
    {
        public static void RegisterUserEndpoints(this IEndpointRouteBuilder erb)
        {
            var users = erb.MapGroup("/users").WithTags("Users");
            users.MapPost("/register", async (CreateUserCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
            });




        }
    }
}
