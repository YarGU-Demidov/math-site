using System;
using System.Threading.Tasks;
using MathSite.Db;

namespace MathSite.Tests.CoreThings
{
    public interface ITestDatabaseFactory : IDisposable
    {
        void ExecuteWithContext(Action<MathSiteDbContext> yourAction);
        Task ExecuteWithContextAsync(Func<MathSiteDbContext, Task> yourAction);
        Task<MathSiteDbContext> GetContext();
        IDisposable OpenConnection();
    }
}