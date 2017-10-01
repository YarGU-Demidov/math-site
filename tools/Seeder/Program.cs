using System;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Db.DataSeeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Seeder
{
    public class Program
    {
        /// <summary>
        ///     Запуск сидера с первым аргументом - connection string
        /// </summary>
        /// <param name="args">нулевой аргумент - connection string</param>
        public static void Main(string[] args)
        {
            var loggerFactory = new LoggerFactory()
                .AddConsole(LogLevel.Information)
                .AddDebug();

            using (loggerFactory)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                try
                {
                    if (args.Length < 1 || string.IsNullOrWhiteSpace(args[0]))
                        throw new ArgumentException("You should specify connection string!");

                    var options = new DbContextOptionsBuilder<MathSiteDbContext>()
                        .UseNpgsql(args[0])
                        .UseLoggerFactory(loggerFactory)
                        .EnableSensitiveDataLogging();

                    using (var context = new MathSiteDbContext(options.Options))
                    {
                        var seeder = new DataSeeder(context, loggerFactory.CreateLogger<DataSeeder>(),
                            GetPasswordManager());
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