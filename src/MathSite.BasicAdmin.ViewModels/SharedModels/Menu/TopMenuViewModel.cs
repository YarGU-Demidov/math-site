using System.Collections.Generic;

namespace MathSite.BasicAdmin.ViewModels.SharedModels.Menu
{
	public class TopMenuViewModel
	{
		public TopMenuViewModel()
		{
		}

		public TopMenuViewModel(IEnumerable<MenuLink> links)
		{
			Links = links;
		}

		public IEnumerable<MenuLink> Links { get; set; } = new List<MenuLink>();
	}
}