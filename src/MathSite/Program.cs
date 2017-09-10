using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace MathSite
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length > 0 && args[0].ToLower() == "seed")
				RunSeeding();
			else
				BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()	
				.Build();

		public static void RunSeeding()
		{
			var appSettingsFile = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

			var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(appSettingsFile));

			settings.ConnectionStrings.TryGetValue("Math", out var connectionString);

			Seeder.Program.Main(new[] { connectionString });
		}
	}
}