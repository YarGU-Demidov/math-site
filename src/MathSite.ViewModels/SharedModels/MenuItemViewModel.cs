namespace MathSite.ViewModels.SharedModels
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel(string name, string href, bool isHeading = false)
            : this(name, href, name, isHeading)
        {
        }

        public MenuItemViewModel(string name, string href, string title, bool isHeading = false)
        {
            Name = name;
            Href = href;
            Title = title;
            IsHeading = isHeading;
        }

        public string Name { get; }
        public string Href { get; }
        public string Title { get; }
        public bool IsHeading { get; }
    }
}