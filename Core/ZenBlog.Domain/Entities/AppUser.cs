using Microsoft.AspNetCore.Identity;

namespace ZenBlog.Domain.Entities
{
    public class AppUser : IdentityUser<string>
    { public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? imageUrl { get; set; }
    
        public IList<Blog> Blogs { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}
