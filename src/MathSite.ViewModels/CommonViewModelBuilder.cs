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
                await SiteSettingsFacade.GetTitleDelimiter(),
                await SiteSettingsFacade.GetSiteName()
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

            var now = DateTime.UtcNow;
            var enteringYear = now.Month > 5 ? now.Year : now.Year - 1;
            var footerMenu =
                new List<IEnumerable<MenuItemViewModel>>
                {
                    new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel("Абитуриентам", "/for-entrants", true),
                        new MenuItemViewModel($"Поступление {enteringYear}", "/how-to-enter"),
                        new MenuItemViewModel("День открытых дверей", "/news/open-doors-day"),
                        new MenuItemViewModel("Олимпиады", "/olimpiada"),
                        new MenuItemViewModel("Школа при факультете", "/for-scholars"),
                        //new MenuItemViewModel("Прием иностранных граждан", "admission-of-foreign-citizens"),
                        //new MenuItemViewModel("Объявления", "math-faculty-ads"),
                        //new MenuItemViewModel("Задать вопросы", "ask-questions"),
                        new MenuItemViewModel("Контакты приемной комиссии", "/kontaktyi-priemnoj-komissii"),
                        //new MenuItemViewModel("Контакты деканата", "contacts")
                    },
                    new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel("Студентам", "/for-students", true),
                        //new MenuItemViewModel("Первокурсникам", "for-freshmen"),
                        //new MenuItemViewModel("Расписание занятий", "timetable-of-classes"),
                        //new MenuItemViewModel("Расписание сессии", "session-schedule"),
                        //new MenuItemViewModel("Библиотека", "library"),
                        //new MenuItemViewModel("Видеотека", "video-library"),
                        new MenuItemViewModel("Кружки", "/kruzhki-dlya-studentov"),
                        new MenuItemViewModel("Кафедры", "/departments"),
                        new MenuItemViewModel("Семинары", "/for-students/seminary"),
                        //new MenuItemViewModel("Курсовые и дипломные", "term-papers-and-thesis-papers"),
                        //new MenuItemViewModel("Важные даты", "important-dates"),
                        //new MenuItemViewModel("Трудоустройство", "employment"),
                        new MenuItemViewModel("Студенческая жизнь", "category/students-activities")
                    },
                    new List<MenuItemViewModel>
                    {
                        //new MenuItemViewModel("История", "history", true),
                        new MenuItemViewModel("Новости", "news", true),
                        new MenuItemViewModel("Кафедры", "/departments", true),
                        //new MenuItemViewModel("Сотрудники", "employees", true),
                        //new MenuItemViewModel("Выпускники", "graduates", true),
                        //new MenuItemViewModel("Партнеры / работодатели", "where-to-work", true),
                        //new MenuItemViewModel("Библиотека", "library", true),
                        //new MenuItemViewModel("Видеотека", "video-library", true),
                        new MenuItemViewModel("Школа", "/for-scholars", true),
                        new MenuItemViewModel("Контакты", "contacts", true)
                    }
                };

            viewModel.MainMenuLinks = footerMenu;
        }
    }
}