using System;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    /// <summary>
    /// </summary>
    public class PostAttachment : Entity
    {
        public bool Allowed { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid FileId { get; set; }
        public File File { get; set; }
    }
}