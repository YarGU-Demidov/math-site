using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;

namespace MathSite.BasicAdmin.ViewModels.Categories
{
    public class CategoryViewModel : AdminPageBaseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }

        public bool DisableAliasInput { get; set; }
    }
}