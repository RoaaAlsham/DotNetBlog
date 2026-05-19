using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Blogs.Commands
{
    public class CreateBlogCommand: IRequest<BaseResult<object>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public string BlogImageUrl { get; set; }

        public Guid CategoryId { get; set; } 

        public string UserId { get; set; }
    }
}
