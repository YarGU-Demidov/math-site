using System;
using System.Collections.Generic;
using System.Text;

namespace MathSite.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PostType
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }
        public List<PostSettings> DefaultPostsSettings { get; set; }
        public List<Post> Posts { get; set; }
    }
}
