using System;
using MathSite.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MathSite
{
	public partial class Startup
	{
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
			IServiceScopeFactory service)
		{
			ConfigureLoggers(loggerFactory);

			service.SeedData();

			var cookieHttpOnly = true;

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
				cookieHttpOnly = false;


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

			ConfigureAuthentication(app, cookieHttpOnly);
			ConfigureRoutes(app);
		}

		private void ConfigureLoggers(ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug(LogLevel.Debug);
		}

		private static void ConfigureAuthentication(IApplicationBuilder app, bool cookieHttpOnly)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationScheme = "Auth",
				LoginPath = new PathString("/login/"),
				AccessDeniedPath = new PathString("/forbidden/"),
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				CookieHttpOnly = cookieHttpOnly,
				LogoutPath = new PathString("/logout/"),
				ReturnUrlParameter = "returnUrl",
				ExpireTimeSpan = TimeSpan.FromDays(365)
			});
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
					new { controller = "Home", action = "Index" }
				);
			});
		}
	}
}