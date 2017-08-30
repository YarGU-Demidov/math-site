using System;
using System.Collections.Generic;

namespace MathSite.ViewModels.Capability
{
	public class CommonPageInfo
	{
		public PageTitle PageTitle { get; }
		public string Description { get; }
		public string Keywords { get; }
		public IEnumerable<MenuItem> MenuItems { get; }

		public Tuple<IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>> FooterMenus { get; }

		public CommonPageInfo(
			PageTitle pageTitle, 
			string description, 
			string keywords,
			IEnumerable<MenuItem> menuItems, 
			Tuple<IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>> footerMenus)
		{
			PageTitle = pageTitle;
			Description = description;
			Keywords = keywords;
			MenuItems = menuItems;
			FooterMenus = footerMenus;
		}
	}
}