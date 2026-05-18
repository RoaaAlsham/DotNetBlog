
using AutoMapper;
using ZenBlog.Application.Features.Blogs.Results;

namespace ZenBlog.Application.Features.Blogs.Mapping
{
    public class BlogMappingProfile: Profile
    {
        public BlogMappingProfile() {
            CreateMap<BlogMappingProfile, GetBlogsQueryResults>();
        }
    }
}
