namespace MathSite.ViewModels.SharedModels
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel(string name, string href, bool isHeading = false, bool newTab = false)
            : this(name, href, name, isHeading, newTab)
        {
        }

        public MenuItemViewModel(string name, string href, string title, bool isHeading = false, bool newTab = false)
        {
            Name = name;
            Href = href;
            Title = title;
            IsHeading = isHeading;
            NewTab = newTab;
        }

        public string Name { get; }
        public string Href { get; }
        public string Title { get; }
        public bool IsHeading { get; }
        public bool NewTab { get; }
    }
}