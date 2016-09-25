using MathSite.Common.Logs;
using ILogger = MathSite.Common.Logs.ILogger;
using MathSite.Db;
using MathSite.Middlewares;
using MathSite.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

			services.AddRouting(options => { options.LowercaseUrls = true; });
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
			IServiceScopeFactory service)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			service.SeedData();

			app.UseAutorizeHandler();

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
				routes.MapRoute(
					"default",
					"{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}