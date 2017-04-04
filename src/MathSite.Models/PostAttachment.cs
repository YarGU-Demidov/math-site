using System;
using System.Collections.Generic;
using System.Text;

namespace MathSite.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PostAttachment
    {
        public Guid Id { get; set; }
        public bool Allowed { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid FileId { get; set; }
        public File File { get; set; }
    }
}
