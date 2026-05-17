using FluentValidation;
using ZenBlog.Application.Base;
namespace ZenBlog.API.CustomMiddlewares
{
    //primary constructor syntax for middleware
    //RequestDelegate is a delegate that represents the next middleware in the pipeline
    public class CustomExceptionHandlingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try { await next(context); }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                var response = new BaseResult<object>()
                {
                    Errors = ex.Errors.GroupBy(x => x.PropertyName).Select(g => new Error
                    {
                        PropertyName = g.Key,
                        ErrorMessage = g.Select(x => x.ErrorMessage).FirstOrDefault()
                    }).ToList()

                };
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (BadHttpRequestException ex) // Thrown when JSON is malformed or binding fails
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                var response = BaseResult<object>.Failure("Invalid request body. Please check your JSON structure and property names.");
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (ArgumentNullException ex) // Thrown when command is null
            {
                // This catches the specific error you were seeing
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                var response = BaseResult<object>.Failure("Missing or invalid request body. Ensure the 'CategoryName' property is provided.");
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var response = BaseResult<object>.Failure("An unexpected error occurred.");
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
