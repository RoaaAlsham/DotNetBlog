using AutoMapper;
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Categories.Commands;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Categories.Handlers
{
    public class RemoveCategoryCommandHandler (IRepository<Category> repository, IUnitOfWork uow): IRequestHandler<RemoveCategoryCommand, BaseResult<bool>>
    {
        public async Task<BaseResult<bool>> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await repository.GetByIdAsync(request.guid, cancellationToken);
            if (category == null)
            {
                return BaseResult<bool>.NotFound($"Category with id {request.guid} not found.");
            }
            repository.DeleteAsync(category);
            var response = await uow.SaveChangesAsync();
            return response 
                ? BaseResult<bool>.Success(true)
                : BaseResult<bool>.Failure("Failed to remove category.");
        }
    }
}
