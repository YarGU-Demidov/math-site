using System;

namespace MathSite.Entities
{
    public class PostKeyword
    {
        public Guid Id { get; set; }
        public Guid KeywordId { get; set; }
        public Keyword Keyword { get; set; }
        public Guid PostSeoSettingsId { get; set; }
        public PostSeoSetting PostSeoSettings { get; set; }
    }
}