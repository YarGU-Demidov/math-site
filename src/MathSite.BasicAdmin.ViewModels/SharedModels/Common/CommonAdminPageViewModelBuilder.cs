using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.SharedModels.Common
{
    public abstract class CommonAdminPageViewModelBuilder
    {
        protected CommonAdminPageViewModelBuilder(ISiteSettingsFacade siteSettingsFacade)
        {
            SiteSettingsFacade = siteSettingsFacade;
        }

        public ISiteSettingsFacade SiteSettingsFacade { get; }

        protected virtual async Task<T> BuildCommonViewModelAsync<T>(Func<MenuLink, bool> markActiveLink)
            where T : CommonAdminPageViewModel, new()
        {
            var viewModel = new T();

            await BuildPageTitleAsync(viewModel);
            await BuildTopMenuAsync(viewModel, markActiveLink);

            return viewModel;
        }

        protected virtual async Task BuildTopMenuAsync<T>(T viewModel, Func<MenuLink, bool> markActiveLink)
            where T : CommonAdminPageViewModel
        {
            viewModel.TopMenu = new List<MenuLink>
            {
                new MenuLink("Dashboard", "/manager/", false),
                new MenuLink("Статьи", "/manager/pages/", false, "Управление статьями", "Articles"),
                new MenuLink("Новости", "/manager/news/", false, "Управление новостями", "News"),
                new MenuLink("Файлы", "/manager/files/", false, "Управление файлами", "Files"),
                new MenuLink("Лица", "/manager/persons/", false, "Управление лицами", "Persons"),
                new MenuLink("Пользователи", "/manager/users/", false, "Управление пользователями", "Users"),
                new MenuLink("Категории", "/manager/categories/", false, "Управление категориями", "Categories"),
                new MenuLink("Настройки", "/manager/settings/", false, "Управление настройками", "Settings")
            };

            foreach (var link in viewModel.TopMenu)
            {
                link.IsActive = markActiveLink(link);

                if(link.IsActive)
                    break;
            }
        }

        protected virtual async Task BuildPageTitleAsync<T>(T viewModel)
            where T : CommonAdminPageViewModel
        {
            var pageTitle = new PageTitleViewModel(
                "",
                await SiteSettingsFacade.GetTitleDelimiter(),
                await SiteSettingsFacade.GetSiteName()
            );

            viewModel.PageTitle = pageTitle;
        }
    }
}