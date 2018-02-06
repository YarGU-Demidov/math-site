using System;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Db.DataSeeding;
using MathSite.Facades;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Tests.CoreThings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MathSite.Tests.Facades
{
    public abstract class FacadesTestsBase<T> : FacadesTestsBase
        where T: class, IFacade
    {
        protected abstract T GetFacade(MathSiteDbContext context, IRepositoryManager manager);
    }

    public class FacadesTestsBase
    {
        private readonly ITestDatabaseFactory _databaseFactory;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public FacadesTestsBase()
            : this(TestSqliteDatabaseFactory.UseDefault())
        {
            MemoryCache = new MemoryCache(new MemoryCacheOptions());
            _loggerFactory = new LoggerFactory().AddDebug();
            _logger = _loggerFactory.CreateLogger("TestsLogger");
        }

        public FacadesTestsBase(ITestDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public IMemoryCache MemoryCache { get; }

        protected void WithRepository(Action<IRepositoryManager> actions)
        {
            _databaseFactory.ExecuteWithContext(context =>
            {
                SeedData(context);
                actions(CreateRepositoryManger(context));
            });
        }

        protected async Task WithRepositoryAsync(Func<IRepositoryManager, MathSiteDbContext, ILogger, Task> actions)
        {
            await _databaseFactory.ExecuteWithContextAsync(async context =>
            {
                SeedData(context);
                await actions(CreateRepositoryManger(context), context, _logger);
            });
        }

        private static IRepositoryManager CreateRepositoryManger(MathSiteDbContext context)
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
                new GroupTypeRepository(context),
                new DirectoriesRepository(context),
                new CategoryRepository(context)
            );
        }

        protected void SeedData(MathSiteDbContext context)
        {
            var seeder = new DataSeeder(context, _loggerFactory.CreateLogger<IDataSeeder>(), new DoubleSha512HashPasswordsManager());
            seeder.Seed();
        }
    }
}