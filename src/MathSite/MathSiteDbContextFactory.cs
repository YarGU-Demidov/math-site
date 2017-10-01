using System.IO;
using MathSite.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;

namespace MathSite
{
    public class MathSiteDbContextFactory : IDesignTimeDbContextFactory<MathSiteDbContext>
    {
        private readonly Settings _settings;
        private readonly string _settingsFile = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        public MathSiteDbContextFactory()
        {
            _settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(_settingsFile));
        }

        /// <inheritdoc />
        public MathSiteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MathSiteDbContext>();
            optionsBuilder.UseNpgsql(
                _settings.ConnectionStrings["Math"],
                builder => builder.MigrationsAssembly("MathSite")
            );
            return new MathSiteDbContext(optionsBuilder.Options);
        }
    }
}