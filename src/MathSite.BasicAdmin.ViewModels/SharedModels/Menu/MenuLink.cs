namespace MathSite.BasicAdmin.ViewModels.SharedModels.Menu
{
    public class MenuLink
    {
        public MenuLink(string displayingTitle, string url, bool isActive)
            : this(displayingTitle, url, isActive, displayingTitle)
        {
        }

        public MenuLink(string displayingTitle, string url, bool isActive, string alt)
            : this(displayingTitle, url, isActive, alt, alt)
        {
        }

        public MenuLink(string displayingTitle, string url, bool isActive, string alt, string alias)
        {
            IsActive = isActive;
            Url = url;
            DisplayingTitle = displayingTitle;
            Alt = alt;
            Alias = alias;
        }


        internal string Alias { get; set; }
        public bool IsActive { get; set; }
        public string Url { get; set; }
        public string DisplayingTitle { get; set; }
        public string Alt { get; set; }

        public override string ToString()
        {
            return $"{nameof(Alias)}: {Alias}, {nameof(IsActive)}: {IsActive}, {nameof(Url)}: {Url}, {nameof(DisplayingTitle)}: {DisplayingTitle}";
        }
    }
}