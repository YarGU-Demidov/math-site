using System.IO;
using System.Text;
using System.Threading.Tasks;
using MathSite.Common.Logs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MathSite.Middlewares
{
	public static class AuthorizeExtensions
	{
		public static IApplicationBuilder UseAutorizeHandler(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<AuthorizeMiddleware>();
		}
	}

	public class AuthorizeMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public AuthorizeMiddleware(RequestDelegate next, ILogger logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			_logger.WriteDebug("Handling request: " + context.Request.Path);
			await _next.Invoke(context);
			_logger.WriteDebug("Finished handling request.");
		}
	}
}