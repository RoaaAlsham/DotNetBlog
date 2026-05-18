

namespace ZenBlog.Domain.Entities
{
    public class Category : Common.BaseEntity
    {
        public string CategoryName { get; set; }

       
        public virtual IList<Blog> Blogs { get; set; }
    }
}
