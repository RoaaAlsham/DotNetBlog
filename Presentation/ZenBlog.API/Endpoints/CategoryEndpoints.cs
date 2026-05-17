
using MediatR;
using ZenBlog.Application.Features.Categories.Queries;
namespace ZenBlog.API.Endpoints
{
   //
    public static class CategoryEndpoints
    {
        //Minimal API endpoints
        public static void RegisterCategoryEndpoints(this IEndpointRouteBuilder erb)
        {//extension method to register category endpoints
            // without (this) extension method, we would have to do something like:
            // CategoryEndpoints.RegisterCategoryEndpoints(app) in Program.cs

            var categories = erb.MapGroup("/categories").WithTags("Categories");// all routes starting with /categories will be grouped together 
            categories.MapGet("", async (IMediator _mediator) =>
            {
                var response = await _mediator.Send(new GetCategoryQuery());
                return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
            });
        
        }
    }
    // Using Controllers would be like: 
    /*
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase {
    private readonly IMediator _m;
    public CategoryController( IMediator m) => _m = m;
    [HttpGet] 
    public async Task<IActionResult> GetAll()
    { var r = await _m.Send( new GetCategoryQuery());
    return r.IsSuccess ? Ok(r.Value) : BadRequest(r.Error);
    }
     */
}
