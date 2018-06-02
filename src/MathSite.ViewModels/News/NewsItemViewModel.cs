using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.ViewModels.News
{
    public class NewsItemViewModel : SecondaryViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string PreviewImageId { get; set; }
        public string PreviewImage2XId { get; set; }
    }
}