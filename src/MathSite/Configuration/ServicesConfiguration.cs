using System;
using MathSite.Common.Crypto;
using MathSite.Core.Auth.Handlers;
using MathSite.Core.Auth.Requirements;
using MathSite.Db;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Common;
using MathSite.Domain.Logic.Files;
using MathSite.Domain.Logic.Groups;
using MathSite.Domain.Logic.Persons;
using MathSite.Domain.Logic.Rights;
using MathSite.Domain.Logic.SiteSettings;
using MathSite.Domain.Logic.Users;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Home;
using MathSite.ViewModels.News;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MathSite
{
	// ReSharper disable once ClassNeverInstantiated.Global
	/// <summary>
	///     Класс загрузчик
	/// </summary>
	public partial class Startup
	{
		/// <summary>
		///     Конфигурация сервисов
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
				.AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
			services.AddRouting(options => { options.LowercaseUrls = true; });

			services.AddMemoryCache();

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
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					var expiration = TimeSpan.FromDays(365);

					options.Cookie.Name = "YSU.Math.Auth";
					options.Cookie.HttpOnly = true;
					options.Cookie.Path = "/";
					options.Cookie.Expiration = expiration;
					options.Cookie.SecurePolicy = CookieSecurePolicy.None;
					options.Cookie.SameSite = SameSiteMode.None;

					options.ExpireTimeSpan = expiration;

					options.AccessDeniedPath = new PathString("/error/forbidden/");
					options.LoginPath = new PathString("/login/");
					options.LogoutPath = new PathString("/logout/");
					options.ReturnUrlParameter = "returnUrl";
				});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("admin",
					builder => builder.Requirements.Add(new SiteSectionAccess(RightAliases.AdminAccess)));
				options.AddPolicy("peronal-page",
					builder => builder.Requirements.Add(new SiteSectionAccess(RightAliases.PanelAccess)));

				options.AddPolicy("logout",
					builder => builder.Requirements.Add(new SiteSectionAccess(RightAliases.LogoutAccess)));
			});

			services.AddScoped<IAuthorizationHandler, SiteSectionAccessHandler>();
		}

		private void ConfigureDependencyInjection(IServiceCollection services)
		{
			services.AddLogging();

			services.AddOptions();
			services.AddSingleton(Configuration);
			services.AddSingleton<IConfiguration>(Configuration);
			services.Configure<Settings>(Configuration);

			services.AddScoped<IPasswordsManager, DoubleSha512HashPasswordsManager>();
			services.AddScoped<IBusinessLogicManager, BusinessLogicManager>();

			// BL
			services.AddScoped<IGroupsLogic, GroupsLogic>();
			services.AddScoped<IPersonsLogic, PersonsLogic>();
			services.AddScoped<IUsersLogic, UsersLogic>();
			services.AddScoped<IFilesLogic, FilesLogic>();
			services.AddScoped<ISiteSettingsLogic, SiteSettingsLogic>();
			services.AddScoped<IRightsLogic, RightsLogic>();

			// Facades
			services.AddScoped<IUserValidationFacade, UserValidationFacade>();
			services.AddScoped<ISiteSettingsFacade, SiteSettingsFacade>();

			// View Models Builders
			services.AddScoped<IHomeViewModelBuilder, HomeViewModelBuilder>();
			services.AddScoped<INewsViewModelBuilder, NewsViewModelBuilder>();
		}

		private void ConfigureEntityFramework(IServiceCollection services)
		{
			services.AddEntityFrameworkNpgsql()
				.AddDbContextPool<MathSiteDbContext>(options =>
				{
					options.UseNpgsql(
						Configuration.GetConnectionString("Math"),
						builder => builder.MigrationsAssembly("MathSite")
					);
				});
		}
	}
}