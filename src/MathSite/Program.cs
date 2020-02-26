using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MathSite.Common.Extensions;
using Microsoft.Extensions.Configuration;
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
            else
                CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Limits.MaxConcurrentConnections = 5000;
                    options.Limits.MaxConcurrentUpgradedConnections = 5000;
                });

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
            var postfixes = GetFilePostfixes();
            var files = GetFilePaths(postfixes);
            var connectionStringKeyName = "Math";
            var connectionString = default(string);
            
            files.Select(GetSettingsFromFile)
                .ToList()
                .ForEach(settings => 
                {
                    if (settings.IsNull())
                        return;

                    if (!settings.ConnectionStrings.ContainsKey(connectionStringKeyName))
                        return;
                    
                    connectionString = settings.ConnectionStrings[connectionStringKeyName];
                });

            return connectionString;
        }

        private static Settings GetSettingsFromFile(string path) 
        {
            var emptyJson = "{}";
            var fileData = File.Exists(path) ? File.ReadAllText(path) : emptyJson;
            
            return JsonConvert.DeserializeObject<Settings>(fileData);
        }

        private static IEnumerable<string> GetFilePostfixes() 
        {
            // порядок имеет значение.
            // чем ближе к началу списка -- тем меньше приоритет у суффикса.
            return new []
            {
                "",
                "Production",
                "Staging",
                "Development"
            };
        }

        private static IEnumerable<string> GetFilePaths(IEnumerable<string> filePostfixes)
        {
            return filePostfixes.Select(postfix => 
            {
                var fileName = postfix.IsNullOrWhiteSpace() 
                    ? "appsettings.json" 
                    : $"appsettings.{postfix}.json";
                
                return Path.Combine(Environment.CurrentDirectory, fileName);
            });
        }
    }
}