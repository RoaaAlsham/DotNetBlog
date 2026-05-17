using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ZenBlog.Application.Features.Categories.Commands;

namespace ZenBlog.Application.Features.Categories.Mapping
{
    public class CategoryMappingProfile: Profile
    {
        public CategoryMappingProfile() {
            CreateMap<Domain.Entities.Category, Results.GetCategoryQueryResult>().ReverseMap();
            // ADD THIS — maps CreateCategoryCommand → Category
            CreateMap<CreateCategoryCommand, Domain.Entities.Category>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));

        }
    }
}
