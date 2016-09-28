using MathSite.Common.Logs;
using MathSite.Db;
using MathSite.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ILogger = MathSite.Common.Logs.ILogger;

namespace MathSite
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", true, true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }


		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			services.AddEntityFramework()
				.AddEntityFrameworkNpgsql()
				.AddDbContext<MathSiteDbContext>(ServiceLifetime.Scoped);

			services.AddTransient<ILogger, ConsoleLogger>();
			services.AddSingleton<IMathSiteDbContext, MathSiteDbContext>();

			services.AddRouting(options => { options.LowercaseUrls = true; });
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
			IServiceScopeFactory service)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug(LogLevel.Debug);

			service.SeedData();

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				LoginPath = new PathString("/login/"),
				AccessDeniedPath = new PathString("/account/forbidden/"),
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				CookieHttpOnly = true,
				LogoutPath = new PathString("/logout/"),
				ReturnUrlParameter = "return_url"
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute("areaRoute",
					"{area:exists}/{controller=Home}/{action=Index}");
				routes.MapRoute(
					"default",
					"{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}