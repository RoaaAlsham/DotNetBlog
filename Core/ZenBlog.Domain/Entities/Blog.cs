using System;
using System.Collections.Generic;
using System.Text;

namespace ZenBlog.Domain.Entities
{
    public class Blog : Common.BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public string BlogImageUrl { get; set; }

        public Guid CategoryId { get; set; } // Foreign key to Category
        public Category Category { get; set; } // Navigation property to Category
        
        public string UserId { get; set; } // Foreign key to User
        public AppUser User { get; set; } // Navigation property to User

        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}
