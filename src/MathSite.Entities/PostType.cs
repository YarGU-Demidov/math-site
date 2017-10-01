using System;
using System.Collections.Generic;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    /// <summary>
    /// </summary>
    public class PostType : EntityWithNameAndAlias
    {
        public Guid DefaultPostsSettingsId { get; set; }
        public PostSetting DefaultPostsSettings { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}