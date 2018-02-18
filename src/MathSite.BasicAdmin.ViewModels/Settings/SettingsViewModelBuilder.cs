using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Common;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Entities;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.Settings
{
    public interface ISettingsViewModelBuilder
    {
        Task<IndexSettingsViewModel> BuildIndexViewModelAsync();
        Task SaveData(User user, IndexSettingsViewModel model);
    }

    public class SettingsViewModelBuilder : AdminPageWithPagingViewModelBuilder, ISettingsViewModelBuilder
    {
        public SettingsViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade
        ) : base(siteSettingsFacade)
        {
        }
        
        protected override Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return Task.FromResult(Enumerable.Empty<MenuLink>());
        }

        public async Task<IndexSettingsViewModel> BuildIndexViewModelAsync()
        {
            var model = await BuildAdminBaseViewModelAsync<IndexSettingsViewModel>(link => link.Alias == "Settings");

            await SetPageTitle(model);
            await SetModelProps(model);

            return model;
        }

        public async Task SaveData(User user, IndexSettingsViewModel model)
        {
            await SiteSettingsFacade.SetSiteName(user.Id, model.SiteName);
            await SiteSettingsFacade.SetDefaultNewsPageTitle(user.Id, model.DefaultTitleForNewsPage);
            await SiteSettingsFacade.SetDefaultHomePageTitle(user.Id, model.DefaultTitleForHomePage);
            await SiteSettingsFacade.SetPerPageCountAsync(user.Id, model.PerPageCount.ToString());
            await SiteSettingsFacade.SetTitleDelimiter(user.Id, model.TitleDelimiter);
        }

        private async Task SetModelProps(IndexSettingsViewModel model)
        {
            model.SiteName = await SiteSettingsFacade.GetSiteName(false);
            model.DefaultTitleForNewsPage = await SiteSettingsFacade.GetDefaultNewsPageTitle(false);
            model.DefaultTitleForHomePage = await SiteSettingsFacade.GetDefaultHomePageTitle(false);
            model.PerPageCount = await SiteSettingsFacade.GetPerPageCountAsync(false);
            model.TitleDelimiter = await SiteSettingsFacade.GetTitleDelimiter(false);
        }

        private Task SetPageTitle(CommonAdminPageViewModel model)
        {
            return Task.Run(() => model.PageTitle.Title = "Настройки сайта");
        }
    }
}