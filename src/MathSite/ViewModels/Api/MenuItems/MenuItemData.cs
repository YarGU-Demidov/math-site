using Newtonsoft.Json;

namespace MathSite.ViewModels.Api.MenuItems
{
	public class MenuItemData
	{
		[JsonProperty("icon")]
		public string Icon;

		[JsonProperty("name")]
		public string Name;

		[JsonProperty("href")]
		public string Href;

		[JsonProperty("subItems")]
		public MenuItemData[][] SubItems;

		public MenuItemData(string icon, string name, string href, MenuItemData[][] items = null)
		{
			Icon = icon;
			Name = name;
			Href = href;
			SubItems = items ?? new MenuItemData[0][];
		}
	}
}