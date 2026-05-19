using AutoMapper;
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Blogs.Queries;
using ZenBlog.Application.Features.Blogs.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Blogs.Handlers
{
    public class GetBlogsQueryHandler(IRepository<Blog> repository, IMapper mapper) : IRequestHandler<GetBlogsQuery, BaseResult<List<GetBlogsQueryResults>>>
    {
        public async Task<BaseResult<List<GetBlogsQueryResults>>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var values = await repository.GetAllAsync(cancellationToken);
            var blogs = mapper.Map<List<GetBlogsQueryResults>>(values);
            return BaseResult<List<GetBlogsQueryResults>>.Success(blogs);
        
        }
    }
}
