using System;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    public class PostSetting : Entity
    {
        public bool IsCommentsAllowed { get; set; }
        public bool CanBeRated { get; set; }
        public bool PostOnStartPage { get; set; }
        public string Layout { get; set; }
        public Post Post { get; set; }
        public PostType PostType { get; set; }
        public Guid? PreviewImageId { get; set; }
        public File PreviewImage { get; set; }
        public DateTime? EventTime { get; set; }
        public string EventLocation { get; set; }
    }
}