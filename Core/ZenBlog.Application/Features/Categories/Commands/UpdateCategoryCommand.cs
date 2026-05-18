using MediatR;
using ZenBlog.Application.Base;

namespace ZenBlog.Application.Features.Categories.Commands
{
    public record UpdateCategoryCommand(Guid Id, string CategoryName) : IRequest<BaseResult<bool>>;
}
