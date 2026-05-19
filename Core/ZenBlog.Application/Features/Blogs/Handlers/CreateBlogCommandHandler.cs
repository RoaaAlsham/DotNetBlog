using AutoMapper;
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Blogs.Commands;
using ZenBlog.Application.Features.Blogs.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Blogs.Handlers
{
    public class CreateBlogCommandHandler(IRepository<Domain.Entities.Blog> repository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateBlogCommand, BaseResult<CreateBlogResult>>
    {
        public async Task<BaseResult<CreateBlogResult>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {

            var blog = mapper.Map<Blog>(request);
            await repository.CreateAsync(blog);
            var saved = await unitOfWork.SaveChangesAsync();
            return saved
                ? BaseResult<CreateBlogResult>.Success(new CreateBlogResult(blog.Id, blog.Title))
                : BaseResult<CreateBlogResult>.Failure("Failed to create blog.");
        }
    }
}
