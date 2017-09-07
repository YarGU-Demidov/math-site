using System;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Db.DataSeeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Seeder
{
	internal class Program
	{
		/// <summary>
		///     Запуск сидера с первым аргументом - connection string
		/// </summary>
		/// <param name="args">нулевой аргумент - connection string</param>
		private static void Main(string[] args)
		{
			var loggerFactory = new LoggerFactory()
				.AddConsole(LogLevel.Trace)
				.AddDebug();

			using (loggerFactory)
			{
				var logger = loggerFactory.CreateLogger<Program>();

				if (args.Length < 1 || string.IsNullOrWhiteSpace(args[0]))
				{
					logger.LogError(
						new ArgumentException("You should specify connection string!"),
						"You forget to specify your connection string!"
					);
					return;
				}

				try
				{
					var options = new DbContextOptionsBuilder<MathSiteDbContext>().UseNpgsql(args[0]);

					using (var context = new MathSiteDbContext(options.Options))
					{
						var seeder = new DataSeeder(context, loggerFactory.CreateLogger<DataSeeder>(), GetPasswordManager());
						seeder.Seed();
					}
				}
				catch (Exception e)
				{
					logger.LogError(e, "Unhandled critical exeption!");
					Console.ReadKey();
				}
			}
		}

		private static IPasswordsManager GetPasswordManager()
		{
			return new DoubleSha512HashPasswordsManager();
		}
	}
}