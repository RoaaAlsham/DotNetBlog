using System;
using System.Collections.Generic;
using System.Text;

namespace ZenBlog.Domain.Entities
{
    public class SocialMedia: Common.BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }

        public string Icon { get; set; }
    }
}
