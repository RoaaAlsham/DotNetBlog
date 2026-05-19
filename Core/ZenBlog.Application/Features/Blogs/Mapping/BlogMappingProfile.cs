
using AutoMapper;
using ZenBlog.Application.Features.Blogs.Commands;
using ZenBlog.Application.Features.Blogs.Results;

namespace ZenBlog.Application.Features.Blogs.Mapping
{
    public class BlogMappingProfile: Profile
    {
        public BlogMappingProfile() {
            CreateMap<Domain.Entities.Blog, GetBlogsQueryResults>().ReverseMap();
            CreateMap<Domain.Entities.Blog, CreateBlogCommand>().ReverseMap();
           

        }
    }
}
