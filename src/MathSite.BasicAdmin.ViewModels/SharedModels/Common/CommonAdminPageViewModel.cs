using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;

namespace MathSite.BasicAdmin.ViewModels.SharedModels.Common
{
    public abstract class CommonAdminPageViewModel
    {
        public PageTitleViewModel PageTitle { get; set; }

        public IEnumerable<MenuLink> TopMenu { get; set; }
    }
}