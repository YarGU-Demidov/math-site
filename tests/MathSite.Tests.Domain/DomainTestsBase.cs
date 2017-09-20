using System;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Tests.CoreThings;

namespace MathSite.Tests.Domain
{
    public abstract class DomainTestsBase
    {
        private readonly ITestDatabaseFactory _databaseFactory;

        public DomainTestsBase()
            : this(TestSqliteDatabaseFactory.UseDefault())
        {
        }

        public DomainTestsBase(ITestDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        protected virtual async Task ExecuteWithContextAsync(Func<MathSiteDbContext, Task> yourAction)
        {
            await _databaseFactory.ExecuteWithContextAsync(yourAction);
        }

        protected virtual void ExecuteWithContext(Action<MathSiteDbContext> yourAction)
        {
            _databaseFactory.ExecuteWithContext(yourAction);
        }
    }
}