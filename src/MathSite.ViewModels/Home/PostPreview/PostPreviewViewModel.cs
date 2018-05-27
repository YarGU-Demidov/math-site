using System;

namespace MathSite.ViewModels.Home.PostPreview
{
    public class PostPreviewViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string PreviewImageId { get; set; }
        public string PreviewImageId2X { get; set; }
        public string Content { get; set; }
        public string PostTypeName { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}