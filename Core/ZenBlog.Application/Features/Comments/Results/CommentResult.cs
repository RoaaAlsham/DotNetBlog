using System;
using System.Collections.Generic;
using System.Text;
using ZenBlog.Application.Base;
using ZenBlog.Application.DTOs.ZenBlog.Application.DTOs;

namespace ZenBlog.Application.Features.Comments.Results
{
    public class CommentResult : BaseDto
    {
        public string Body { get; set; }
        public Guid BlogId { get; set; }
        public string UserId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public UserDto User { get; set; }
        public IList<CommentResult> Replies { get; set; } = []; 
    }
}
