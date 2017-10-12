﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace MathSite
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Any(s => s == "seed"))
                RunSeeding();
            else if (args.Any(s => s == "import-news"))
                RunImportNews();
            else if (args.Any(s => s == "import-pages"))
                RunImportStaticPages();
            else
                BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) => 
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(GetBindUrl(args))
                .Build();

        private static string GetBindUrl(IEnumerable<string> args)
        {
            var defaultUrl = $"http://{IPAddress.Any}:5000";

            return args.FirstOrDefault(s => s.StartsWith("http://") || s.StartsWith("https://")) ?? defaultUrl;
        }

        public static void RunSeeding()
        {
            var connectionString = GetCurrentConnectionString();

            Seeder.Program.Main(new[] {connectionString});
        }

        public static void RunImportNews()
        {
            var connectionString = GetCurrentConnectionString();

            NewsImporter.Program.Main(new[] {connectionString});
        }

        private static void RunImportStaticPages()
        {
            var connectionString = GetCurrentConnectionString();

            StaticImporter.Program.Main(new[] {connectionString});
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