using System;
using System.Collections.Generic;
using System.Text;

namespace ZenBlog.Application.Features.Comments.Results
{
   
        public sealed record CreateCommentResult(Guid Id, string Body, Guid BlogId, Guid? ParentCommentId);
    
}
