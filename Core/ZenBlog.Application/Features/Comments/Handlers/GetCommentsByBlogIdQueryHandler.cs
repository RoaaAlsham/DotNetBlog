// Handlers/GetCommentsByBlogIdQueryHandler.cs
using AutoMapper;
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Comments.Queries;
using ZenBlog.Application.Features.Comments.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Comments.Handlers
{
    public class GetCommentsByBlogIdQueryHandler(IRepository<Comment> repo, IMapper mapper)
        : IRequestHandler<GetCommentsByBlogIdQuery, BaseResult<IEnumerable<CommentResult>>>
    {
        public async Task<BaseResult<IEnumerable<CommentResult>>> Handle(
            GetCommentsByBlogIdQuery request, CancellationToken cancellationToken)
        {
            // Only fetch top-level comments — replies are nested inside via Replies collection
            var comments = await repo.GetAllWithIncludesAsync(
                c => c.BlogId == request.BlogId && c.ParentCommentId == null,
                cancellationToken,
                c => c.User,
                c => c.Replies);

            return BaseResult<IEnumerable<CommentResult>>
                .Success(mapper.Map<IEnumerable<CommentResult>>(comments));
        }
    }
}