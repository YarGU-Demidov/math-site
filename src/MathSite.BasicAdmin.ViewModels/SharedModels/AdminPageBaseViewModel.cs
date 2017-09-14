using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;

namespace MathSite.BasicAdmin.ViewModels.SharedModels
{
	public class AdminPageBaseViewModel
	{
		public AdminPageBaseViewModel(PageTitleViewModel pageTitle, TopMenuViewModel topMenu)
		{
			PageTitle = pageTitle;
			TopMenu = topMenu;
		}

		public PageTitleViewModel PageTitle { get; set; }

		public TopMenuViewModel TopMenu { get; set; }
	}
}