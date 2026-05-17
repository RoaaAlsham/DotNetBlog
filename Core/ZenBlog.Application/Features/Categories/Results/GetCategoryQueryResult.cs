using System;
using System.Collections.Generic;
using System.Text;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Categories.Results
{
    public class GetCategoryQueryResult
    {
        public string CategoryName { get; set; }
        public IList<Blog> Blogs { get; set; }//will be used in get blcok query result format
    }
}
