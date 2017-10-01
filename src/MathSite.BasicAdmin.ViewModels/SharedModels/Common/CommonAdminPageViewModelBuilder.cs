using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Db.DataSeeding.StaticData;
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
                new MenuLink("Dashboard", "/manager", false),
                new MenuLink("Статьи", "/manager/pages", false, "Управление статьями", "Articles"),
                new MenuLink("Новости", "/manager/news", false, "Управление новостями", "News"),
                new MenuLink("Файлы", "/manager", false, "Управление файлами", "Files"),
                new MenuLink("Пользователи", "/manager", false, "Управление пользователями", "Users"),
                new MenuLink("Настройки", "/manager", false, "Управление настройками", "Settings")
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
                await SiteSettingsFacade[SiteSettingsNames.TitleDelimiter],
                await SiteSettingsFacade[SiteSettingsNames.SiteName]
            );

            viewModel.PageTitle = pageTitle;
        }
    }
}