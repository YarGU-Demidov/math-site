using System.Collections.Generic;
using MathSite.ViewModels.Home.PostPreview;

namespace MathSite.ViewModels.SharedModels.SecondaryPage
{
    public class SecondaryViewModel : CommonViewModel
    {
        public IEnumerable<PostPreviewViewModel> Featured { get; set; }
    }
}