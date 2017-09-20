using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;

namespace MathSite.BasicAdmin.ViewModels.SharedModels
{
    public class AdminPageBaseViewModel
    {
        public AdminPageBaseViewModel(PageTitleViewModel pageTitle, IEnumerable<MenuLink> topMenu)
        {
            PageTitle = pageTitle;
            TopMenu = topMenu;
        }

        public PageTitleViewModel PageTitle { get; set; }

        public IEnumerable<MenuLink> TopMenu { get; set; }

        public IEnumerable<MenuLink> LeftMenu { get; set; }
    }
}