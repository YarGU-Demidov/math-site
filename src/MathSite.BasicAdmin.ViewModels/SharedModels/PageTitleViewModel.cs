namespace MathSite.BasicAdmin.ViewModels.SharedModels
{
	public class PageTitleViewModel
	{
		public string Title { get;  set; }
		public string Delimiter { get; set; }
		public string SiteName { get; set; }

		public PageTitleViewModel(string title, string delimiter, string siteName)
		{
			Title = title;
			Delimiter = delimiter;
			SiteName = siteName;
		}

		public override string ToString() =>
			string.IsNullOrWhiteSpace(Title)
				? SiteName
				: Title + Delimiter + SiteName;
	}
}