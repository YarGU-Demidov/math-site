using System;
using Microsoft.Extensions.DependencyInjection;

namespace MathSite.Common
{
    public static class LazyRegisterExtension
    {
        public static IServiceCollection AddLazyProvider(this IServiceCollection services)
        {
            return services.AddTransient(typeof(Lazy<>), typeof(Lazier<>));
        }
    }

    public class Lazier<T> : Lazy<T> where T : class
    {
        public Lazier(IServiceProvider provider)
            : base(provider.GetRequiredService<T>)
        {
        }
    }
}