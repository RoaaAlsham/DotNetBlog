

using ZenBlog.Application.Base;

namespace ZenBlog.Application.Features.Categories.Results
{
    public class GetCategoryByIdQueryResult: BaseDto

    {
        public string CategoryName { get; set; }
    }
}
//why it inherits from BaseDto? because we want to return the Id,
//CreatedAt and UpdatedAt properties of the category in the response,
//and these properties are defined in the BaseDto class, so by inheriting
//from BaseDto, we can easily include these properties in our
//GetCategoryByIdQueryResult class without having to redefine them.
//This promotes code reusability and keeps our codebase clean and organized.

//why we didn't inherit from BaseResult? because we want to return only the
//category data in the response, and we don't need to include any additional
//information such as errors or success status, which are typically included
//in a BaseResult. By not inheriting from BaseResult, we can keep our
//GetCategoryByIdQueryResult class focused on representing the category data
//without any unnecessary properties.