using AutoMapper;
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Blogs.Commands;

namespace ZenBlog.Application.Features.Blogs.Handlers
{
    public class CreateBlogCommandHandler(IRepository<Domain.Entities.Blog> repository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateBlogCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = mapper.Map<Domain.Entities.Blog>(request);
            await repository.CreateAsync(blog);
            var result = await unitOfWork.SaveChangesAsync();
            return result ? BaseResult<object>.Success(blog) : BaseResult<object>.Failure("Failed to create blog.");
        }
    }
}
