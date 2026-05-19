// Handlers/DeleteCommentCommandHandler.cs
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Comments.Commands;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Comments.Handlers
{
    public class DeleteCommentCommandHandler(IRepository<Comment> repo, IUnitOfWork uow)
        : IRequestHandler<RemoveCommentCommand, BaseResult<bool>>
    {
        public async Task<BaseResult<bool>> Handle(
            RemoveCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await repo.GetByIdAsync(request.Id, cancellationToken);
            if (comment == null)
                return BaseResult<bool>.NotFound($"Comment with id {request.Id} not found.");

            await repo.DeleteAsync(comment);
            var saved = await uow.SaveChangesAsync();

            return saved
                ? BaseResult<bool>.Success(true)
                : BaseResult<bool>.Failure("Failed to delete comment.");
        }
    }
}