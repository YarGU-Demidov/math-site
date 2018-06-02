using MathSite.BasicAdmin.ViewModels.Categories;
﻿using MathSite.BasicAdmin.ViewModels.Events;
using MathSite.BasicAdmin.ViewModels.Files;
using MathSite.BasicAdmin.ViewModels.Home;
using MathSite.BasicAdmin.ViewModels.News;
using MathSite.BasicAdmin.ViewModels.Pages;
using MathSite.BasicAdmin.ViewModels.Persons;
using MathSite.BasicAdmin.ViewModels.Professors;
using MathSite.BasicAdmin.ViewModels.Settings;
using MathSite.BasicAdmin.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;

namespace MathSite.BasicAdmin.ViewModels
{
    public static class BasicAdminViewModelBuildersRegiserExtension
    {
        /// <summary>
        ///     Добавляем ViewModelBuilder-ы
        /// </summary>
        /// <param name="services">
        ///     The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add the
        ///     services to.
        /// </param>
        /// <returns>
        ///     The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> so that additional calls can
        ///     be chained.
        /// </returns>
        public static IServiceCollection AddBasicAdminViewModelBuilders(this IServiceCollection services)
        {
            return services.AddScoped<IDashboardPageViewModelBuilder, DashboardPageViewModelBuilder>()
                .AddScoped<INewsManagerViewModelBuilder, NewsManagerViewModelBuilder>()
                .AddScoped<IPagesManagerViewModelBuilder, PagesManagerViewModelBuilder>()
                .AddScoped<IEventsManagerViewModelBuilder, EventsManagerViewModelBuilder>()
                .AddScoped<IPersonsManagerViewModelBuilder, PersonsManagerViewModelBuilder>()
                .AddScoped<IFilesManagerViewModelBuilder, FilesManagerViewModelBuilder>()
                .AddScoped<IUsersManagerViewModelBuilder, UsersManagerViewModelBuilder>()
                .AddScoped<ICategoriesViewModelBuilder, CategoriesViewModelBuilder>()
                .AddScoped<IProfessorViewModelBuilder, ProfessorViewModelBuilder>()
                .AddScoped<ISettingsViewModelBuilder, SettingsViewModelBuilder>();
        }
    }
}