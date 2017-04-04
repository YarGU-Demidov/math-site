using System;

namespace MathSite.Models
{
    public class PostCategory
    {
        public Guid Id { get; set; }
        public Category Category { get; set; }
        public Post Post { get; set; }
    }
}
