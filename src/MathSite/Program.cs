using System.IO;
using MathSite.Common.Logs;
using Microsoft.AspNetCore.Hosting;

namespace MathSite
{
	public class Program
	{
		static Program()
		{
		}

		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}