namespace MathSite.BasicAdmin.ViewModels.SharedModels
{
    public class PageTitleViewModel
    {
        public PageTitleViewModel(string title, string delimiter, string siteName)
        {
            Title = title;
            Delimiter = delimiter;
            SiteName = siteName;
        }

        public string Title { get; set; }
        public string Delimiter { get; set; }
        public string SiteName { get; set; }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Title)
                ? SiteName
                : Title + Delimiter + SiteName;
        }
    }
}