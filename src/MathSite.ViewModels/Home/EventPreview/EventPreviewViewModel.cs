using System;
using MathSite.ViewModels.Home.PostPreview;

namespace MathSite.ViewModels.Home.EventPreview
{
    public class EventPreviewViewModel : PostPreviewViewModel
    {
        public string Location { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}