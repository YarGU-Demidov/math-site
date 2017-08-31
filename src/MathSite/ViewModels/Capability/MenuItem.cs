using System.Collections.Generic;

namespace MathSite.ViewModels.Capability
{
	public class MenuItem
	{
		public string Name { get; }
		public string Href { get; }
		public string Title { get; }

		public IEnumerable<MenuItem> Subitems { get; }

		public MenuItem(string name, string href, IEnumerable<MenuItem> subitems = null)
			: this(name, href, name, subitems)
		{
		}

		public MenuItem(string name, string href, string title, IEnumerable<MenuItem> subitems = null)
		{
			Name = name;
			Href = href;
			Title = title;
			Subitems = subitems;
		}
	}
}