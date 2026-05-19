
using MediatR;
using ZenBlog.Application.Base;

namespace ZenBlog.Application.Features.Categories.Commands
{
  
    public record CreateCategoryCommand : IRequest<BaseResult<object>>
    {
        public string CategoryName { get; init; } = string.Empty;
    }

}
/*Why record?
 In CQRS, a Command is:

A one-time intent ("update this category")
Immutable — it should never change after being dispatched (With a class, you'd have to manually use init or private set on every property.)
Value-based — two commands with the same data are the same command (compare by value, not by reference)
 */