using System;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    public class PostCategory : Entity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public bool IsChecked { get; set; }
    }
}