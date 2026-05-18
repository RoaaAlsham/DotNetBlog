using MediatR;
using ZenBlog.Application.Base;

namespace ZenBlog.Application.Features.Categories.Commands
{
    public record RemoveCategoryCommand(Guid guid) : IRequest<BaseResult<bool>>;
}
