using System;
using System.Collections.Generic;
using System.Text;

namespace ZenBlog.Application.Features.Users.Results
{
    public sealed record CreateUserResult // sealed: prevent inheritance, record: value-based, immutable
    (
        string Id, string Username, string Email,string FullName, DateTime CreatedAt
    );
}
