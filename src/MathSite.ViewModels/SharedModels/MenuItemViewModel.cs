using System.Collections.Generic;

namespace MathSite.ViewModels.SharedModels
{
	public class MenuItemViewModel
	{
		public string Name { get; }
		public string Href { get; }
		public string Title { get; }

		public IEnumerable<MenuItemViewModel> Subitems { get; }

		public MenuItemViewModel(string name, string href, IEnumerable<MenuItemViewModel> subitems = null)
			: this(name, href, name, subitems)
		{
		}

		public MenuItemViewModel(string name, string href, string title, IEnumerable<MenuItemViewModel> subitems = null)
		{
			Name = name;
			Href = href;
			Title = title;
			Subitems = subitems;
		}
	}
}