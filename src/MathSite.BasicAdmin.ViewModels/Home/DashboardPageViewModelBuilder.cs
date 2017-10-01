using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.Common;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.Home
{
    public interface IDashboardPageViewModelBuilder
    {
        Task<DashboardPageViewModel> BuilDashboardPageViewModel();
    }

    public class DashboardPageViewModelBuilder : CommonAdminPageViewModelBuilder, IDashboardPageViewModelBuilder
    {
        public DashboardPageViewModelBuilder(ISiteSettingsFacade siteSettingsFacade) : base(siteSettingsFacade)
        {
        }


        public async Task<DashboardPageViewModel> BuilDashboardPageViewModel()
        {
            var model = await BuildCommonViewModelAsync<DashboardPageViewModel>(link => link.Alias == "Dashboard");

            model.PageTitle.Title = "Dashboard";

            return model;
        }
    }
}