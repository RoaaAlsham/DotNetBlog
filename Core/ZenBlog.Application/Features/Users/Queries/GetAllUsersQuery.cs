using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ZenBlog.Application.Base;
using ZenBlog.Application.Features.Users.Results;

namespace ZenBlog.Application.Features.Users.Queries
{
    public class GetAllUsersQuery: IRequest<BaseResult<IEnumerable<GetAllUsersQueryResult>>>
    {
    }
}
