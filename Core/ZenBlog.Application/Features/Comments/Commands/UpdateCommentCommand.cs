using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Features.Comments.Results;

namespace ZenBlog.Application.Features.Comments.Commands
{
    public class UpdateCommentCommand : IRequest<BaseResult<CommentResult>>
    {
        public Guid Id { get; set; }
        public required string Body { get; set; }
    }
}