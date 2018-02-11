using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace MathSite
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Any(s => s == "seed"))
                RunSeeding();
            else if (args.Any(s => s == "import-news"))
                await RunImportNews();
            else if (args.Any(s => s == "import-pages"))
                await RunImportStaticPages();
            else if (args.Any(s => s == "KeyGenerate"))
                await RunKeyGenerator();
            else
                BuildWebHost(args).Run();
        }

        private static async Task void RunKeyGenerator()
        {
            await KeyGenerator.Program.Main(new string[0]);
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Limits.MaxConcurrentConnections = 5000;
                    options.Limits.MaxConcurrentUpgradedConnections = 5000;
                })
                .Build();

        public static void RunSeeding()
        {
            var connectionString = GetCurrentConnectionString();

            Seeder.Program.Main(new[] {connectionString});
        }

        public static async Task RunImportNews()
        {
            var connectionString = GetCurrentConnectionString();

            await NewsImporter.Program.Main(new[] {connectionString});
        }

        private static async Task RunImportStaticPages()
        {
            var connectionString = GetCurrentConnectionString();

            await StaticImporter.Program.Main(new[] {connectionString});
        }

        private static string GetCurrentConnectionString()
        {
            var appSettingsFile = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

            var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(appSettingsFile));
            settings.ConnectionStrings.TryGetValue("Math", out var connectionString);

            return connectionString;
        }
    }
}