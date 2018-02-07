using System.Collections.Generic;
using MathSite.ViewModels.Home.EventPreview;
using MathSite.ViewModels.Home.PostPreview;
using MathSite.ViewModels.Home.StudentActivityPreview;

namespace MathSite.ViewModels.Home
{
    public class HomeIndexViewModel : CommonViewModel
    {
        public IEnumerable<PostPreviewViewModel> Posts { get; set; }
        public IEnumerable<EventPreviewViewModel> Events { get; set; }
        public IEnumerable<StudentActivityViewModel> StudentsActivities { get; set; }
    }
}