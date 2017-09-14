namespace MathSite.BasicAdmin.ViewModels.SharedModels.Menu
{
	public class MenuLink
	{
		public bool IsActive { get; set; }
		public string Url { get; set; }
		public string DisplayingTitle { get; set; }
		public string Alt { get; set; }

		public MenuLink(string displayingTitle, string url, bool isActive)
			: this(displayingTitle, url, isActive, displayingTitle)
		{
		}

		public MenuLink(string displayingTitle, string url, bool isActive, string alt)
		{
			IsActive = isActive;
			Url = url;
			DisplayingTitle = displayingTitle;
			Alt = alt;
		}
	}
}