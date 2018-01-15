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

        protected ISiteSettingsFacade SiteSettingsFacade { get; }


        protected virtual async Task<T> BuildCommonViewModelAsync<T>()
            where T : CommonViewModel, new()
        {
            var viewModel = new T();

            await BuildPageTitleAsync(viewModel);
            await BuildDescriptionAsync(viewModel);
            await BuildKeywordsAsync(viewModel);
            await BuildTopMenuAsync(viewModel);
            await BuildMainMenuAsync(viewModel);

            return viewModel;
        }

        protected virtual async Task BuildPageTitleAsync<T>(T viewModel)
            where T : CommonViewModel
        {
            var pageTitle = new PageTitleViewModel(
                "",
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
                new MenuItemViewModel("Контакты", "contacts")
            };

            viewModel.TopMenuLinks = menu;
        }

        protected virtual async Task BuildMainMenuAsync<T>(T viewModel)
            where T : CommonViewModel
        {
            var footerMenu =
                new List<IEnumerable<MenuItemViewModel>>
                {
                    new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel("Абитуриентам", "for-entrants", true),
                        new MenuItemViewModel("Поступление 2017", "#"),
                        new MenuItemViewModel("День открытых дверей", "#"),
                        new MenuItemViewModel("Олимпиады", "#"),
                        new MenuItemViewModel("Школа при факультете", "#"),
                        new MenuItemViewModel("Прием иностранных граждан", "#"),
                        new MenuItemViewModel("Объявления", "#"),
                        new MenuItemViewModel("Задать вопросы", "#"),
                        new MenuItemViewModel("Контакты приемной комиссии", "#"),
                        new MenuItemViewModel("Контакты деканата", "#")
                    },
                    new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel("Студентам", "for-students", true),
                        new MenuItemViewModel("Первокурсникам", "#"),
                        new MenuItemViewModel("Расписание занятий", "#"),
                        new MenuItemViewModel("Расписание сессии", "#"),
                        new MenuItemViewModel("Библиотека", "#"),
                        new MenuItemViewModel("Видеотека", "#"),
                        new MenuItemViewModel("Кружки", "#"),
                        new MenuItemViewModel("Кафедры", "#"),
                        new MenuItemViewModel("Семинары", "#"),
                        new MenuItemViewModel("Курсовые и&nbsp;дипломные", "#"),
                        new MenuItemViewModel("Важные даты", "#"),
                        new MenuItemViewModel("Трудоустройство", "#"),
                        new MenuItemViewModel("Студенческая жизнь", "#")
                    },
                    new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel("История", "#", true),
                        new MenuItemViewModel("Новости", "#", true),
                        new MenuItemViewModel("Кафедры", "#", true),
                        new MenuItemViewModel("Сотрудники", "#", true),
                        new MenuItemViewModel("Выпускники", "#", true),
                        new MenuItemViewModel("Партнеры/работодатели", "#", true),
                        new MenuItemViewModel("Библиотека", "#", true),
                        new MenuItemViewModel("Видеотека", "#", true),
                        new MenuItemViewModel("Школа", "for-scholars", true),
                        new MenuItemViewModel("Контакты", "#", true)
                    }
                };

            viewModel.MainMenuLinks = footerMenu;
        }
    }
}