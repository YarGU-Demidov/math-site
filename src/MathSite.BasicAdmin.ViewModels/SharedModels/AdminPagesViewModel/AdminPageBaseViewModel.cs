using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.Common;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;

namespace MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel
{
    public class AdminPageBaseViewModel : CommonAdminPageViewModel
    {
        public IEnumerable<MenuLink> LeftMenu { get; set; }
    }
}