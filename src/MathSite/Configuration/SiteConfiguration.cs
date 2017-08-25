using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace MathSite
{
	public partial class Startup
	{
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			var isDevelopmentEnv = env.IsDevelopment();

			ConfigureLoggers(loggerFactory, isDevelopmentEnv);

			if (isDevelopmentEnv)
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();


				app.UseCors(builder =>
				{
					builder.AllowAnyOrigin();
					builder.AllowAnyHeader();
					builder.AllowAnyMethod();
					builder.AllowCredentials();
				});
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStatusCodePagesWithRedirects("~/errors/{0}");

			ConfigureAuthentication(app);
			ConfigureRoutes(app);
		}

		private void ConfigureLoggers(ILoggerFactory loggerFactory, bool isDebug)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));

			if (isDebug)
				loggerFactory.AddDebug(LogLevel.Debug);
		}

		private static void ConfigureAuthentication(IApplicationBuilder app)
		{
			app.UseAuthentication();
		}

		private static void ConfigureRoutes(IApplicationBuilder app)
		{
			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute("areaRoute",
					"{area:exists}/{controller=Home}/{action=Index}");
				routes.MapRoute(
					"default",
					"{controller=Home}/{action=Index}/{id?}");
				routes.MapAreaRoute(
					"PersonalPageRoutes",
					"personal-page",
					"personal-page/{*pageAdress}",
					new {controller = "Home", action = "Index"}
				);
			});
		}
	}
}