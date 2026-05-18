namespace ZenBlog.API.Endpoints
{
    public static class UserEndpoints
    {
        public static void RegisterUserEndpoints(this IEndpointRouteBuilder erb)
        {
            var users = erb.MapGroup("/users").WithTags("Users");

            

        }
    }
}
