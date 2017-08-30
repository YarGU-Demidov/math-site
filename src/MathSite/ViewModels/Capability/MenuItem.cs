namespace MathSite.ViewModels.Capability
{
	public class MenuItem
	{
		public string Name { get; }
		public string Href { get; }
		public string Title { get; }

		public MenuItem(string name, string href)
			: this(name, href, name)
		{
		}

		public MenuItem(string name, string href, string title)
		{
			Name = name;
			Href = href;
			Title = title;
		}
	}
}