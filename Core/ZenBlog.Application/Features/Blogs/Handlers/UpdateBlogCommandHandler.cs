using AutoMapper;
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Blogs.Commands;
using ZenBlog.Application.Features.Blogs.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Blogs.Handlers
{
    public class UpdateBlogCommandHandler(IRepository<Blog> repo, IMapper mapper, IUnitOfWork uow)
    : IRequestHandler<UpdateBlogCommand, BaseResult<GetBlogsQueryResult>>
    {
        public async Task<BaseResult<GetBlogsQueryResult>> Handle(
            UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await repo.GetByIdAsync(request.Id, cancellationToken);
            if (blog == null)
                return BaseResult<GetBlogsQueryResult>.NotFound($"Blog with id {request.Id} not found.");

            mapper.Map(request, blog); // updates existing blog in-place
            await repo.UpdateAsync(blog);
            var saved = await uow.SaveChangesAsync();

            if (!saved)
                return BaseResult<GetBlogsQueryResult>.Failure("Failed to update blog.");

            return BaseResult<GetBlogsQueryResult>.Success(mapper.Map<GetBlogsQueryResult>(blog));
        }
    }
}
