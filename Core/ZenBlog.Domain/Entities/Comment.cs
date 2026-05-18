using ZenBlog.Domain.Entities.Common;

namespace ZenBlog.Domain.Entities
{
    // BaseEntity stays as-is ✓

    public class Comment : BaseEntity
    {
        public string Body { get; set; }

        // Which blog this comment belongs to
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }

        // Who wrote it
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }

        // Self-referencing: null = top-level comment, set = it's a reply
        public Guid? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

        // All replies to this comment
        public virtual IList<Comment> Replies { get; set; } = new List<Comment>();
    }
}
/*
 // Top-level comment: ParentCommentId = null
// Reply: ParentCommentId = some Comment's Id

// To check if a comment is a reply:
bool isReply = comment.ParentCommentId.HasValue;

// To get only top-level comments for a blog:
var topLevel = db.Comments
    .Where(c => c.BlogId == blogId && c.ParentCommentId == null)
    .Include(c => c.Replies)
        .ThenInclude(r => r.User)
    .Include(c => c.User)
    .ToListAsync();
 */
