// Queries/GetCommentsByBlogIdQuery.cs
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Features.Comments.Results;

namespace ZenBlog.Application.Features.Comments.Queries
{
    public record GetCommentsByBlogIdQuery(Guid BlogId)
        : IRequest<BaseResult<IEnumerable<CommentResult>>>;
}