using System;
using System.Collections.Generic;
using System.Text;
using ZenBlog.Application.Base;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Categories.Results
{
    public class GetCategoryQueryResult : BaseDto
    {
        public string CategoryName { get; set; }
        public IList<Blog> Blogs { get; set; }//will be used in get blck query result format
    }
}
