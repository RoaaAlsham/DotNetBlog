using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ZenBlog.Application.Base;

namespace ZenBlog.Application.Features.Blogs.Commands
{
    public record RemoveBlogCommand(Guid Id) : IRequest<BaseResult<bool>>;
    
}
