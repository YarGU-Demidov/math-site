using System;
using System.Collections.Generic;

namespace MathSite.ViewModels.Capability
{
	public class MainPageViewModel : CommonPageInfo
	{
		public MainPageViewModel(
			PageTitle pageTitle, 
			string description, 
			string keywords, 
			IEnumerable<MenuItem> menuItems, 
			Tuple<IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>> footerMenus,
			IEnumerable<PostPreviewViewModel> posts) 
			: base(pageTitle, description, keywords, menuItems, footerMenus)
		{
			Posts = posts;
		}

		public IEnumerable<PostPreviewViewModel> Posts { get; }
	}
}