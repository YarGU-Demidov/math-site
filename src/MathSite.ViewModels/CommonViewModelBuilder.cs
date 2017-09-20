using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.SharedModels;

namespace MathSite.ViewModels
{
    public abstract class CommonViewModelBuilder
    {
        protected CommonViewModelBuilder(ISiteSettingsFacade siteSettingsFacade)
        {
            SiteSettingsFacade = siteSettingsFacade;
        }

        public ISiteSettingsFacade SiteSettingsFacade { get; }

        protected abstract string PageTitle { get; set; }

        protected virtual async Task<T> BuildCommonViewModelAsync<T>()
            where T : CommonViewModel, new()
        {
            var viewModel = new T();

            await BuildPageTitleAsync(viewModel);
            await BuildDescriptionAsync(viewModel);
            await BuildKeywordsAsync(viewModel);
            await BuildTopMenuAsync(viewModel);
            await BuildFooterMenuAsync(viewModel);

            return viewModel;
        }

        protected virtual async Task BuildPageTitleAsync<T>(T viewModel)
            where T : CommonViewModel
        {
            var pageTitle = new PageTitleViewModel(
                PageTitle,
                await SiteSettingsFacade[SiteSettingsNames.TitleDelimiter],
                await SiteSettingsFacade[SiteSettingsNames.SiteName]
            );

            viewModel.PageTitle = pageTitle;
        }

        protected virtual async Task BuildDescriptionAsync<T>(T viewModel)
            where T : CommonViewModel
        {
            viewModel.Description = "Главная страница сайта Математического факультета ЯрГУ";
        }

        protected virtual async Task BuildKeywordsAsync<T>(T viewModel)
            where T : CommonViewModel
        {
            viewModel.Keywords = "Математика, ЯрГУ, Матфак";
        }

        protected virtual async Task BuildTopMenuAsync<T>(T viewModel)
            where T : CommonViewModel
        {
            var menu = new List<MenuItemViewModel>
            {
                new MenuItemViewModel("Абитуриентам", "for-entrants"),
                new MenuItemViewModel("Студентам", "for-students"),
                new MenuItemViewModel("Школа", "for-scholars"),
                new MenuItemViewModel("Контакты", "contacts")
            };

            viewModel.MenuItems = menu;
        }

        protected virtual async Task BuildFooterMenuAsync<T>(T viewModel)
            where T : CommonViewModel
        {
            var footerMenu =
                new Tuple<IEnumerable<MenuItemViewModel>, IEnumerable<MenuItemViewModel>, IEnumerable<MenuItemViewModel>
                    ,
                    IEnumerable<MenuItemViewModel>>(
                    new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel("Абитуриентам", "for-entrants"),
                        new MenuItemViewModel("Кафедры", "departments")
                    },
                    new List<MenuItemViewModel> {new MenuItemViewModel("Студентам", "for-students")},
                    new List<MenuItemViewModel> {new MenuItemViewModel("Школа", "for-scholars")},
                    new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel("Контакты", "contacts"),
                        new MenuItemViewModel("math@uniyar.ac.ru", "mailto:math@uniyar.ac.ru"),
                        new MenuItemViewModel("www.math.uniyar.ac.ru", "http://www.math.uniyar.ac.ru"),
                        new MenuItemViewModel("vk.com/math.uniyar.abitur", "https://vk.com/math.uniyar.abitur")
                    }
                );

            viewModel.FooterMenus = footerMenu;
        }
    }
}