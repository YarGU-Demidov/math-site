using MathSite.Facades.Categories;
using MathSite.Facades.FileSystem;
using MathSite.Facades.Groups;
using MathSite.Facades.Persons;
using MathSite.Facades.PostCategories;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MathSite.Facades
{
    public static class FacadesRegisterExtension
    {
        /// <summary>
        ///     Добавляем Фасады.
        /// </summary>
        /// <param name="services">
        ///     The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add the
        ///     services to.
        /// </param>
        /// <returns>
        ///     The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> so that additional calls can
        ///     be chained.
        /// </returns>
        public static IServiceCollection AddFacades(this IServiceCollection services)
        {
            return services.AddScoped<IUserValidationFacade, UserValidationFacade>()
                .AddScoped<ISiteSettingsFacade, SiteSettingsFacade>()
                .AddScoped<IPostsFacade, PostsFacade>()
                .AddScoped<IPersonsFacade, PersonsFacade>()
                .AddScoped<IFileFacade, FileFacade>()
                .AddScoped<IDirectoryFacade, DirectoryFacade>()
                .AddScoped<ICategoryFacade, CategoryFacade>()
                .AddScoped<IPostCategoryFacade, PostCategoryFacade>()
                .AddScoped<IGroupsFacade, GroupsFacade>()
                .AddScoped<IUsersFacade, UsersFacade>();
        }
    }
}