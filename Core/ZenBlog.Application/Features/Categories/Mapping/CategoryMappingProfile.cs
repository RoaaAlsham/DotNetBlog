using AutoMapper;
using ZenBlog.Application.DTOs;
using ZenBlog.Application.Features.Categories.Commands;
using ZenBlog.Application.Features.Categories.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Categories.Mapping
{
    public class CategoryMappingProfile: Profile
    {
        public CategoryMappingProfile() {
            CreateMap<Category, Results.GetCategoryQueryResult>().ReverseMap();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
            CreateMap<Blog, BlogDto>();
        }
    }
}
