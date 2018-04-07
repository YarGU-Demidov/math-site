using Microsoft.Extensions.DependencyInjection;

namespace MathSite.Repository.Core
{
    public static class RepositoryRegisterExtension
    {
        /// <summary>
        ///     Добавляем репозитории.
        /// </summary>
        /// <param name="services">
        ///     The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add the
        ///     services to.
        /// </param>
        /// <returns>
        ///     The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> so that additional calls can
        ///     be chained.
        /// </returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IRepositoryManager, RepositoryManager>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IPostCategoryRepository, PostCategoryRepository>()
                .AddScoped<IGroupsRepository, GroupsRepository>()
                .AddScoped<IPersonsRepository, PersonsRepository>()
                .AddScoped<IUsersRepository, UsersRepository>()
                .AddScoped<IFilesRepository, FilesRepository>()
                .AddScoped<IDirectoriesRepository, DirectoriesRepository>()
                .AddScoped<ISiteSettingsRepository, SiteSettingsRepository>()
                .AddScoped<IRightsRepository, RightsRepository>()
                .AddScoped<IPostsRepository, PostsRepository>()
                .AddScoped<IPostSeoSettingsRepository, PostSeoSettingsRepository>()
                .AddScoped<IPostSettingRepository, PostSettingRepository>()
                .AddScoped<IPostTypeRepository, PostTypeRepository>()
                .AddScoped<IGroupTypeRepository, GroupTypeRepository>();
        }
    }
}