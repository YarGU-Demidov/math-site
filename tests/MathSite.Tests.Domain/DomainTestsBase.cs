using System;
using System.Threading.Tasks;
using MathSite.Db;

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

		protected virtual async Task ExecuteWithContextAsync(Func<IMathSiteDbContext, Task> yourAction)
		{
			await _databaseFactory.ExecuteWithContextAsync(yourAction);
		}

		protected virtual void ExecuteWithContext(Action<IMathSiteDbContext> yourAction)
		{
			_databaseFactory.ExecuteWithContext(yourAction);
		}
	}
}