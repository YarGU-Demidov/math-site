using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging
{
    public abstract class AdminPageWithPagingViewModelBuilder : AdminPageBaseViewModelBuilder
    {
        public AdminPageWithPagingViewModelBuilder(ISiteSettingsFacade siteSettingsFacade) : base(siteSettingsFacade)
        {
        }

        protected virtual async Task<T> BuildAdminPageWithPaging<T>(Func<MenuLink, bool> markActiveLinkInTopMenu, Func<MenuLink, bool> markActiveLinkInLeftMenu, int currentPage, int pagesCount, int perPage)
            where T : AdminPageWithPagingViewModel, new()
        {
            var model = await BuildAdminBaseViewModelAsync<T>(markActiveLinkInTopMenu, markActiveLinkInLeftMenu);

            model.CurrentPage = currentPage;
            model.PagesCount = pagesCount;
            model.PerPage = perPage;

            return model;
        }
    }
}