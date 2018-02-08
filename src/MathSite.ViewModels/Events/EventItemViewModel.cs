using MathSite.ViewModels.News;

namespace MathSite.ViewModels.Events
{
    public class EventItemViewModel : NewsItemViewModel
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
    }
}