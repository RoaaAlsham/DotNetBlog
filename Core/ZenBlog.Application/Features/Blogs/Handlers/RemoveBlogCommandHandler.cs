using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Blogs.Commands;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Blogs.Handlers
{
    public class RemoveBlogCommandHandler(IRepository<Blog> repo, IUnitOfWork unitOfWork) : IRequestHandler<RemoveBlogCommand, BaseResult<bool>>
    {
        public async Task<BaseResult<bool>> Handle(RemoveBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await repo.GetByIdAsync(request.Id, cancellationToken);
            if (blog == null)
            {
                return BaseResult<bool>.NotFound($"Blog with id {request.Id} not found.");
            }
            await repo.DeleteAsync(blog);
            var saved = await unitOfWork.SaveChangesAsync();
            return saved ? BaseResult<bool>.Success(true) : BaseResult<bool>.Failure("Failed to delete the blog.");
        }
    }
}
