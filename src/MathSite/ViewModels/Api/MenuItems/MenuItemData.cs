using Newtonsoft.Json;

namespace MathSite.ViewModels.Api.MenuItems
{
	public class MenuItemData
	{
		[JsonProperty("href")]
		public string Href;

		[JsonProperty("icon")]
		public string Icon;

		[JsonProperty("name")]
		public string Name;

		[JsonProperty("subItems")]
		public MenuItemData[][] SubItems;

		public MenuItemData(string name, string href, string icon = "", MenuItemData[][] items = null)
		{
			Icon = icon;
			Name = name;
			Href = href;
			SubItems = items ?? new MenuItemData[0][];
		}

		public MenuItemData(string name, string href, string icon, MenuItemData[] items)
			: this(name, href, icon, new MenuItemData[0][])
		{
			SubItems = items == null
				? new MenuItemData[0][]
				: new[] {items};
		}

		public MenuItemData(string name, string href, MenuItemData[] items) 
			: this(name, href, "", items)
		{
		}
	}
}