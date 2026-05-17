
using MediatR;
using ZenBlog.Application.Base;

namespace ZenBlog.Application.Features.Categories.Commands
{
    // CreateCategoryCommand.cs
    public record CreateCategoryCommand : IRequest<BaseResult<bool>>
    {
        public string CategoryName { get; init; } = string.Empty;
    }

}
