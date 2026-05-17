using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZenBlog.Application.Features.Categories.Mapping
{
    public class CategoryMappingProfile: Profile
    {
        public CategoryMappingProfile() {
            CreateMap<Domain.Entities.Category, Results.GetCategoryQueryResult>().ReverseMap();
        }
    }
}
