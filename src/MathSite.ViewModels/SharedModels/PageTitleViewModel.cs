namespace MathSite.ViewModels.SharedModels
{
    public class PageTitleViewModel
    {
        public PageTitleViewModel(string title, string delimiter, string siteName, bool siteNameFirst = false)
        {
            Title = title;
            Delimiter = delimiter;
            SiteName = siteName;
            SiteNameFirst = siteNameFirst;
        }

        public string Title { get; set; }
        public string Delimiter { get; set; }
        public string SiteName { get; set; }

        public bool SiteNameFirst { get; set; }

        public override string ToString()
        {
            if (Title != null)
                return SiteNameFirst
                    ? SiteName + Delimiter + Title
                    : Title + Delimiter + SiteName;

            return SiteName;
        }
    }
}