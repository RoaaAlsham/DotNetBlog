using MediatR;
using Microsoft.AspNetCore.Identity;
using ZenBlog.Application.Base;
using ZenBlog.Application.Features.Users.Queries;
using ZenBlog.Application.Features.Users.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Users.Handlers
{
    public class GetAllUsersQueryHandler(UserManager<AppUser> userManager)
        : IRequestHandler<GetAllUsersQuery, BaseResult<IEnumerable<GetAllUsersQueryResult>>>
    {
        public async Task<BaseResult<IEnumerable<GetAllUsersQueryResult>>> Handle(
            GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = userManager.Users;

            var result = users.Select(u => new GetAllUsersQueryResult(
                Id: u.Id,
                Username: u.UserName!,
                Email: u.Email!,
                FullName: $"{u.FirstName} {u.LastName}",
                ImageUrl: u.ImageUrl
            ));

            return BaseResult<IEnumerable<GetAllUsersQueryResult>>.Success(result);
        }
    }
}