using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ZenBlog.Application.Base;
using ZenBlog.Application.Features.Blogs.Results;

namespace ZenBlog.Application.Features.Blogs.Queries
{
  public record GetBlogsQuery: IRequest<BaseResult<List<GetBlogsQueryResults>>>
    {
    }
}
