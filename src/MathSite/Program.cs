using System.IO;
using MathSite.Common.Logs;
using Microsoft.AspNetCore.Hosting;

namespace MathSite
{
	public class Program
	{
		public static readonly ILogger Logger;

		static Program()
		{
			Logger = new CompositeLogger(new ConsoleLogger());
		}

		public static void Main(string[] args)
		{
			Logger.WriteInfo("Started!");

			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();

			Logger.WriteInfo("Exit...");
		}
	}
}