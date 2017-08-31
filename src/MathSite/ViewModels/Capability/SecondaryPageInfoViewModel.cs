using System;
using System.Collections.Generic;

namespace MathSite.ViewModels.Capability
{
	public class SecondaryPageInfoViewModel : CommonPageInfo
	{
		public SecondaryPageInfoViewModel(
			PageTitle pageTitle,
			string description,
			string keywords,
			IEnumerable<MenuItem> menuItems,
			Tuple<IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>> footerMenus,
			IEnumerable<PostPreviewViewModel> featured, 
			IEnumerable<MenuItem> sidebarMenuItems)
			: base(pageTitle, description, keywords, menuItems, footerMenus)
		{
			Featured = featured;
			SidebarMenuItems = sidebarMenuItems;
		}

		public IEnumerable<PostPreviewViewModel> Featured { get; }
		public IEnumerable<MenuItem> SidebarMenuItems { get; }
	}
}