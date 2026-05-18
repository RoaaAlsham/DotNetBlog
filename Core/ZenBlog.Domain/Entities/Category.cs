

namespace ZenBlog.Domain.Entities
{
    public class Category : Common.BaseEntity
    {
        public string CategoryName { get; set; }

       
        public IList<Blog> Blogs { get; set; }
    }
}
