using MathSite.Common.Logs;
using MathSite.Core.Auth.Handlers;
using MathSite.Core.Auth.Requirements;
using MathSite.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace MathSite
{
	public partial class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddRouting(options => { options.LowercaseUrls = true; });

			ConfigureEntityFramework(services);
			ConfigureDependencyInjection(services);
			ConfigureAuthPolices(services);
			ConfigureRazorViews(services);
		}

		private static void ConfigureRazorViews(IServiceCollection services)
		{
			services.Configure<RazorViewEngineOptions>(options =>
			{
				options.AreaViewLocationFormats.Add("/Areas/PersonalPage/Views/{1}/{0}.cshtml");
				options.AreaViewLocationFormats.Add("/Areas/PersonalPage/Views/Shared/{0}.cshtml");
			});
		}

		private static void ConfigureAuthPolices(IServiceCollection services)
		{
			services.AddAuthorization(options =>
			{
				options.AddPolicy("admin", builder => builder.Requirements.Add(new SiteSectionAccess("Admin Access")));
				options.AddPolicy("logout", builder => builder.Requirements.Add(new SiteSectionAccess("Logout Access")));
			});

			services.AddSingleton<IAuthorizationHandler, SiteSectionAccessHandler>();
		}

		private static void ConfigureDependencyInjection(IServiceCollection services)
		{
			services.AddTransient<ILogger, ConsoleLogger>();
			services.AddSingleton<IMathSiteDbContext, MathSiteDbContext>();
		}

		private static void ConfigureEntityFramework(IServiceCollection services)
		{
			services.AddEntityFramework()
							.AddEntityFrameworkNpgsql()
							.AddDbContext<MathSiteDbContext>(ServiceLifetime.Scoped);
		}
	}
}