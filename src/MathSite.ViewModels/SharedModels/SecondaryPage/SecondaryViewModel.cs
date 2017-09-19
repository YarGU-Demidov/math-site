using System.Collections.Generic;
using MathSite.ViewModels.SharedModels.PostPreview;

namespace MathSite.ViewModels.SharedModels.SecondaryPage
{
    public class SecondaryViewModel : CommonViewModel
    {
        public IEnumerable<PostPreviewViewModel> Featured { get; set; }
        public IEnumerable<MenuItemViewModel> SidebarMenuItems { get; set; }
    }
}