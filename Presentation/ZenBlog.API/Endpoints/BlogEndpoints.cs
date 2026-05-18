using MediatR;
using ZenBlog.Application.Features.Blogs.Queries;

namespace ZenBlog.API.Endpoints
{
    public static class BlogEndpoints
    {
        public static void RegisterBlogEndpoints(this IEndpointRouteBuilder erb)
        {
             var blogs = erb.MapGroup("/blogs").WithTags("Blogs");

            blogs.MapGet("", async (IMediator _mediator) =>
            {
                var response = await _mediator.Send(new GetBlogsQuery());
               return response.IsSuccess ? Results.Ok(response.Data) : Results.BadRequest(response.Errors);
            });
        }
    }
}
