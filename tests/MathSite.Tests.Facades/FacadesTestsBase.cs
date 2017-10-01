using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Db.DataSeeding;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Tests.CoreThings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MathSite.Tests.Facades
{
    public class FacadesTestsBase
    {
        private readonly ITestDatabaseFactory _databaseFactory;
        private readonly ILogger _logger = new LoggerFactory().CreateLogger("TestsLogger");

        public FacadesTestsBase()
            : this(TestSqliteDatabaseFactory.UseDefault())
        {
            MemoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public FacadesTestsBase(ITestDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public IMemoryCache MemoryCache { get; }

        protected void WithRepository(Action<IRepositoryManager> actions)
        {
            _databaseFactory.ExecuteWithContext(context => { actions(CreateRepositoryManger(context)); });
        }

        protected async Task WithRepositoryAsync(Func<IRepositoryManager, MathSiteDbContext, ILogger, Task> actions)
        {
            await _databaseFactory.ExecuteWithContextAsync(async context =>
            {
                await actions(CreateRepositoryManger(context), context, _logger);
            });
        }

        private IRepositoryManager CreateRepositoryManger(MathSiteDbContext context)
        {
            return new RepositoryManager(
                new GroupsRepository(context),
                new PersonsRepository(context),
                new UsersRepository(context),
                new FilesRepository(context),
                new SiteSettingsRepository(context),
                new RightsRepository(context),
                new PostsRepository(context),
                new PostSeoSettingsRepository(context),
                new PostSettingRepository(context),
                new PostTypeRepository(context),
                new GroupTypeRepository(context)
            );
        }

        protected void SeedData(IEnumerable<ISeeder> seeders)
        {
            foreach (var seeder in seeders)
            {
                using (seeder)
                {
                    seeder.Seed();
                }
            }
        }
    }
}