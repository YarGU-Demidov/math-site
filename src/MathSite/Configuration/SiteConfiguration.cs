using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Logging;

// ReSharper disable ArgumentsStyleOther
// ReSharper disable ArgumentsStyleStringLiteral

namespace MathSite
{
	public partial class Startup
	{
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			var isDevelopmentEnv = env.IsDevelopment();

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

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
				app.UseExceptionHandler("/home/error");
			}

			app.UseStatusCodePagesWithReExecute("/error/{0}");

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
				routes.MapRoute(
					"areaRoute",
					"{area:exists}/{controller=Home}/{action=Index}"
				);

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}"
				);

				//новости
				routes.MapRoute(
					name: "News",
					template: "news/{*query}",
					defaults: new {controller = "News", action = "Index"}
				);

				// статические страницы
				routes.MapRoute(
					name: "Pages",
					template: "{*query}",
					defaults: new {controller = "Pages", action = "Index"}
				);

				routes.MapAreaRoute(
					name: "PersonalPageRoutes",
					areaName: "personal-page",
					template: "personal-page/{*pageAdress}",
					defaults: new {controller = "Home", action = "Index"}
				);
			});
		}
	}
}