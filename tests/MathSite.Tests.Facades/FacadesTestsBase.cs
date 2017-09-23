using System;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Tests.CoreThings;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Tests.Facades
{
    public class FacadesTestsBase
    {
        private readonly ITestDatabaseFactory _databaseFactory;

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

        protected void WithLogic(Action<IRepositoryManager> actions)
        {
            _databaseFactory.ExecuteWithContext(context => { actions(CreateBusinessLogicManger(context)); });
        }

        protected async Task WithLogicAsync(Func<IRepositoryManager, Task> actions)
        {
            await _databaseFactory.ExecuteWithContextAsync(async context =>
            {
                await actions(CreateBusinessLogicManger(context));
            });
        }

        private IRepositoryManager CreateBusinessLogicManger(MathSiteDbContext context)
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
                new PostTypeRepository(context)
            );
        }
    }
}