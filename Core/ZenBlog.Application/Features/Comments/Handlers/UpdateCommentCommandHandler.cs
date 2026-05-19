// Handlers/UpdateCommentCommandHandler.cs
using AutoMapper;
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Comments.Commands;
using ZenBlog.Application.Features.Comments.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Comments.Handlers
{
    public class UpdateCommentCommandHandler(
        IRepository<Comment> repo,
        IUnitOfWork uow,
        IMapper mapper)
        : IRequestHandler<UpdateCommentCommand, BaseResult<CommentResult>>
    {
        public async Task<BaseResult<CommentResult>> Handle(
            UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await repo.GetSingleWithIncludesAsync(
                c => c.Id == request.Id,
                cancellationToken,
                c => c.User,
                c => c.Replies);

            if (comment == null)
                return BaseResult<CommentResult>.NotFound($"Comment with id {request.Id} not found.");

            mapper.Map(request, comment);
            await repo.UpdateAsync(comment);
            var saved = await uow.SaveChangesAsync();

            if (!saved)
                return BaseResult<CommentResult>.Failure("Failed to update comment.");

            return BaseResult<CommentResult>.Success(mapper.Map<CommentResult>(comment));
        }
    }
}