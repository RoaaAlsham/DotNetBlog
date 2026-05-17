using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ZenBlog.Application.Base;
using ZenBlog.Application.Features.Categories.Results;

namespace ZenBlog.Application.Features.Categories.Queries
{
    public class GetCategoryQuery : IRequest<BaseResult<List<GetCategoryQueryResult>>>
    {

    }
}
