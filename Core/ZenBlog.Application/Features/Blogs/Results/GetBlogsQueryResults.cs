

using ZenBlog.Application.Base;
using ZenBlog.Application.Features.Categories.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Blogs.Results
{
    public class GetBlogsQueryResults: BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public string BlogImageUrl { get; set; }

        public Guid CategoryId { get; set; } // Foreign key to Category
        public  GetCategoryQueryResult Category { get; set; } // Navigation property to Category

        public string UserId { get; set; } // Foreign key to User
        //public  AppUser User { get; set; } // Navigation property to User

        //public  IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}
