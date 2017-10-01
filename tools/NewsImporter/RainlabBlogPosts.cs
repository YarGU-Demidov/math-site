using System;

namespace Importer
{
    public class RainlabBlogPost
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public string ContentHtml { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public bool Published { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool FrontPageVisible { get; set; }
    }
}