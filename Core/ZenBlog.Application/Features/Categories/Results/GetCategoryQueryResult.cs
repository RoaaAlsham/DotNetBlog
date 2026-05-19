using ZenBlog.Application.Base;
using ZenBlog.Application.DTOs;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Categories.Results
{
    public class GetCategoryQueryResult : BaseDto
    {
        public string CategoryName { get; set; }
        public IList<BlogDto> Blogs { get; set; } = [];
    }
}
