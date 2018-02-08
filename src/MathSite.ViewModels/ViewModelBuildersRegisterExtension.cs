using MathSite.ViewModels.Events;
using MathSite.ViewModels.Home;
using MathSite.ViewModels.Home.EventPreview;
using MathSite.ViewModels.Home.PostPreview;
using MathSite.ViewModels.Home.StudentActivityPreview;
using MathSite.ViewModels.News;
using MathSite.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace MathSite.ViewModels
{
    public static class ViewModelBuildersRegisterExtension
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
        public static IServiceCollection AddViewModelBuilders(this IServiceCollection services)
        {
            return services.AddScoped<IHomeViewModelBuilder, HomeViewModelBuilder>()
                .AddScoped<INewsViewModelBuilder, NewsViewModelBuilder>()
                .AddScoped<IEventsViewModelBuilder, EventsViewModelBuilder>()
                .AddScoped<IPagesViewModelBuilder, PagesViewModelBuilder>()
                .AddScoped<IStudentActivityPreviewViewModelBuilder, StudentActivityViewModelBuilder>()
                .AddScoped<IPostPreviewViewModelBuilder, PostPreviewViewModelBuilder>()
                .AddScoped<IEventPreviewViewModelBuilder, EventPreviewViewModelBuilder>();
        }
    }
}