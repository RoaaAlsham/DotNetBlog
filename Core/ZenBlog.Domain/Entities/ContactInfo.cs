using System;
using System.Collections.Generic;
using System.Text;

namespace ZenBlog.Domain.Entities
{
    public class ContactInfo : Common.BaseEntity
    {
        public string Address { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        public string MapUrl { get; set; }
    }
}
