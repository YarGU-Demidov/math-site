using System;
using System.Collections.Generic;

namespace MathSite.ViewModels.Capability
{
	public class NewsPageInfo : SecondaryPageInfoViewModel
	{
		public NewsPageInfo(
			PageTitle pageTitle,
			string description,
			string keywords,
			IEnumerable<MenuItem> menuItems,
			Tuple<IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>> footerMenus,
			IEnumerable<PostPreviewViewModel> featured, 
			IEnumerable<PostPreviewViewModel> posts,
			IEnumerable<MenuItem> sidebarMenuItems)
			: base(pageTitle, description, keywords, menuItems, footerMenus, featured, sidebarMenuItems)
		{
			Posts = posts;
		}

		public IEnumerable<PostPreviewViewModel> Posts { get; }
	}
}