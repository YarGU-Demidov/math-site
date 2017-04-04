using MathSite.Common.Crypto;
using MathSite.Core.Auth.Handlers;
using MathSite.Core.Auth.Requirements;
using MathSite.Db;
using MathSite.Db.DataSeeding;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Db.EntityConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MathSite
{
	// ReSharper disable once ClassNeverInstantiated.Global
	/// <summary>
	///		Класс загрузчик
	/// </summary>
	public partial class Startup
	{
		/// <summary>
		///		Конфигурация сервисов
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
				.AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
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
				options.AddPolicy("admin", builder => builder.Requirements.Add(new SiteSectionAccess(RightsAliases.AdminAccess)));
				options.AddPolicy("peronal-page", builder => builder.Requirements.Add(new SiteSectionAccess(RightsAliases.PanelAccess)));

				options.AddPolicy("logout", builder => builder.Requirements.Add(new SiteSectionAccess(RightsAliases.LogoutAccess)));
			});

			services.AddSingleton<IAuthorizationHandler, SiteSectionAccessHandler>();
		}

		private static void ConfigureDependencyInjection(IServiceCollection services)
		{
			services.AddLogging();

			services.AddScoped<IDataSeeder, DataSeeder>();
			services.AddScoped<IPasswordHasher, Passwords>();
			services.AddScoped<IEntitiesConfigurator, EntitiesConfigurator>();
		}

		private void ConfigureEntityFramework(IServiceCollection services)
		{
			services.AddEntityFramework()
				.AddEntityFrameworkNpgsql()
				.AddDbContext<MathSiteDbContext>(options =>
				{
					options.UseNpgsql(
						Configuration.GetConnectionString("Math"), 
						builder => builder.MigrationsAssembly("MathSite")
					);
				});
		}
	}
}