using System;
using System.Threading.Tasks;
using MathSite.Db;

namespace MathSite.Tests.CoreThings
{
	public interface ITestDatabaseFactory : IDisposable
	{
		void ExecuteWithContext(Action<IMathSiteDbContext> yourAction);
		Task ExecuteWithContextAsync(Func<IMathSiteDbContext, Task> yourAction);
		Task<IMathSiteDbContext> GetContext();
		IDisposable OpenConnection();
	}
}