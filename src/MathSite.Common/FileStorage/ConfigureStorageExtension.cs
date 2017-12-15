using Microsoft.Extensions.DependencyInjection;

namespace MathSite.Common.FileStorage
{
    public static class ConfigureStorageExtension
    {
        public static IServiceCollection AddStorage<T>(this IServiceCollection serviceCollection)
            where T: class, IFileStorage
        {
            return serviceCollection.AddTransient<IFileStorage, T>();
        }
    }
}