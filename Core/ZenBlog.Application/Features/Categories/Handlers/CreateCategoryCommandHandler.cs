using AutoMapper;
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Categories.Commands;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Categories.Handlers;

public class CreateCategoryCommandHandler(
    IRepository<Category> repository,
    IUnitOfWork ufw,
    IMapper mapper)
    : IRequestHandler<CreateCategoryCommand, BaseResult<object>>  
{
    public async Task<BaseResult<object>> Handle(
        CreateCategoryCommand request,           
        CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request);

        await repository.CreateAsync(category);
        var result= await ufw.SaveChangesAsync();

        return result? BaseResult<object>.Success(category)
            : BaseResult<object>.Failure("Failed to create category");
    }
}