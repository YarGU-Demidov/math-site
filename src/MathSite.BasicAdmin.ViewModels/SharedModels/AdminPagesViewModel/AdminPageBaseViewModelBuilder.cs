using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.Common;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel
{
    public abstract class AdminPageBaseViewModelBuilder : CommonAdminPageViewModelBuilder
    {
        public AdminPageBaseViewModelBuilder(ISiteSettingsFacade siteSettingsFacade) : base(siteSettingsFacade)
        {
        }

        protected virtual async Task<T> BuildAdminBaseViewModelAsync<T>(Func<MenuLink, bool> markActiveLinkInTopMenu, Func<MenuLink, bool> markActiveLinkInLeftMenu)
            where T : AdminPageBaseViewModel, new()
        {
            var viewModel = await BuildCommonViewModelAsync<T>(markActiveLinkInTopMenu);

            await BuildLeftMenuAsync(viewModel, markActiveLinkInLeftMenu);

            return viewModel;
        }

        protected abstract Task<IEnumerable<MenuLink>> GetLeftMenuLinks();


        protected virtual async Task BuildLeftMenuAsync<T>(T viewModel, Func<MenuLink, bool> markActiveLink)
            where T : AdminPageBaseViewModel
        {
            viewModel.LeftMenu = await GetLeftMenuLinks();

            foreach (var link in viewModel.LeftMenu)
            {
                link.IsActive = markActiveLink(link);

                if (link.IsActive)
                    break;
            }
        }
    }
}