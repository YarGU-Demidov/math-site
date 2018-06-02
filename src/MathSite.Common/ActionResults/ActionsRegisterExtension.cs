using Microsoft.Extensions.DependencyInjection;

namespace MathSite.Common.ActionResults
{
    public static class ActionsRegisterExtension
    {
        public static IServiceCollection AddActionResultExecutors(this IServiceCollection services)
        {
            return services.AddTransient<FileContentInlineResultExecutor>()
                .AddTransient<FileStreamInlineResultExecutor>();
        }
    }
}