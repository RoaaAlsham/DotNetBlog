using AutoMapper;
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Comments.Commands;
using ZenBlog.Application.Features.Comments.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Comments.Handlers
{
    public class CreateCommentCommandHandler(IRepository<Comment> repo,
        IUnitOfWork unitOfWork, IMapper mapper) :
        IRequestHandler<CreateCommentCommand, BaseResult<CreateCommentResult>>
    {
        public async Task<BaseResult<CreateCommentResult>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
           var comment = mapper.Map<Comment>(request);
            await repo.CreateAsync(comment);
            var saved = await unitOfWork.SaveChangesAsync();

            if (!saved) { 
                return BaseResult<CreateCommentResult>.Failure("Failed to create comment.");
            }
            return BaseResult<CreateCommentResult>.Success(new CreateCommentResult(comment.Id, comment.Body, 
                comment.BlogId, comment.ParentCommentId));
        }
    }
}
