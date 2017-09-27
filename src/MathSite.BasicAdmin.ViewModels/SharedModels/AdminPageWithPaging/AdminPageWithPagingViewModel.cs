using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;

namespace MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging
{
    public class AdminPageWithPagingViewModel : AdminPageBaseViewModel
    {
        public int ItemsCount { get; set; }

        public int CurrentPage { get; set; }
    }
}