using System.Collections.Generic;
using MathSite.ViewModels.Home.PostPreview;
using MathSite.ViewModels.SharedModels;
using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.ViewModels.Events
{
    public class EventsIndexViewModel : SecondaryViewModel
    {
        public IEnumerable<PostPreviewViewModel> Posts { get; set; }
        public PaginatorViewModel Paginator { get; set; }
    }
}