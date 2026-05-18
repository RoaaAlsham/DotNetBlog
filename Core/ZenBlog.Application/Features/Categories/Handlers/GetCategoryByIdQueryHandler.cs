using AutoMapper;
using MediatR;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Application.Features.Categories.Queries;
using ZenBlog.Application.Features.Categories.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Categories.Handlers
{
    public class GetCategoryByIdQueryHandler
        (IRepository<Category> repository, IMapper mapper)
        : IRequestHandler<GetCategoryByIdQuery,
            BaseResult<GetCategoryByIdQueryResult>>
    {
        public async Task<BaseResult<GetCategoryByIdQueryResult>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await repository.GetByIdAsync(request.id, cancellationToken);
            if (category == null) { 
                return BaseResult<GetCategoryByIdQueryResult>.NotFound($"Category with id {request.id} not found.");
            }
            var response = mapper.Map<GetCategoryByIdQueryResult>(category);
            return BaseResult<GetCategoryByIdQueryResult>.Success(response);
        }
    }
}
