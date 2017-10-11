﻿using MathSite.BasicAdmin.ViewModels.Home;
using MathSite.BasicAdmin.ViewModels.News;
using MathSite.BasicAdmin.ViewModels.Pages;
using MathSite.BasicAdmin.ViewModels.Persons;
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
                .AddScoped<IPersonsManagerViewModelBuilder, PersonsManagerViewModelBuilder>();
        }
    }
}