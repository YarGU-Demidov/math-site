using System;
using System.Collections.Generic;

namespace MathSite.Models
{
    public class PostSeoSettings
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public List<PostKeywords> PostKeywords { get; set; }
    }
}
