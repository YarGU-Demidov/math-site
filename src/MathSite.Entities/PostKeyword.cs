using System;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    public class PostKeyword : Entity
    {
        public Guid KeywordId { get; set; }
        public Keyword Keyword { get; set; }
        public Guid PostSeoSettingsId { get; set; }
        public PostSeoSetting PostSeoSettings { get; set; }
    }
}