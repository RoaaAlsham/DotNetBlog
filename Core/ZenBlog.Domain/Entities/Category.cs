

namespace ZenBlog.Domain.Entities
{
    public class Category : Common.BaseEntity
    {
        public string CategoryName { get; set; }

        //it has IList<Category> during the first migration by accident
        public IList<Blog> Blogs { get; set; }
    }
}
