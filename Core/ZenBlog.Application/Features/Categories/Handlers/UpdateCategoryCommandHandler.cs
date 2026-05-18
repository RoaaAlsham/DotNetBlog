using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Categories.Commands;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Categories.Handlers
{
    public class UpdateCategoryCommandHandler(IRepository <Category> repository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, BaseResult<bool>>
    {
        public async Task<BaseResult<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await repository.GetByIdAsync(request.Id,cancellationToken);
            if (category == null) { 
                return BaseResult<bool>.Failure($"Category with ID {request.Id} not found.");
            }
            mapper.Map(request, category);
            // Without mapper — manual copies all matching fields from request → category
            //category.Name = request.Name;
            //category.Description = request.Description;
            await repository.UpdateAsync(category);
            var response = await unitOfWork.SaveChangesAsync();
            return response ? BaseResult<bool>.Success(true) : BaseResult<bool>.Failure("Failed to update category.");
        }
    }
}
