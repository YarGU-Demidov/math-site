using System;
using System.Data.Common;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Tests.CoreThings
{
    public class TestDbContext : MathSiteDbContext
    {
        private readonly bool _ignore;

        public TestDbContext(DbContextOptions options, bool ignore = false) : base(options)
        {
            _ignore = ignore;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            if (!_ignore)
                return;
            
            modelBuilder.Ignore<Professor>();
        }
    }

    public abstract class TestDatabaseFactory : ITestDatabaseFactory
    {
        protected virtual bool IgnoreSQLiteWrongData { get; } = false;

        protected readonly DbConnection Connection;

        private MathSiteDbContext _context;

        protected TestDatabaseFactory()
        {
        }

        public TestDatabaseFactory(DbConnection connection)
        {
            Connection = connection;
        }

        public void Dispose()
        {
            Connection?.Close();
            _context?.Dispose();
        }

        public IDisposable OpenConnection()
        {
            Connection?.Open();

            return Connection;
        }

        public async Task<MathSiteDbContext> GetContext()
        {
            _context = new TestDbContext(GetContextOptions(), IgnoreSQLiteWrongData);

            await _context.Database.EnsureCreatedAsync();
            await _context.Database.MigrateAsync();

            return _context;
        }

        public void ExecuteWithContext(Action<MathSiteDbContext> yourAction)
        {
            using (OpenConnection())
            {
                var contextGetterTask = GetContext();
                contextGetterTask.Wait();

                using (var context = contextGetterTask.Result)
                {
                    yourAction(context);
                }
            }
        }

        public async Task ExecuteWithContextAsync(Func<MathSiteDbContext, Task> yourAction)
        {
            using (OpenConnection())
            using (var context = await GetContext())
            {
                await yourAction(context);
            }
        }

        protected abstract DbContextOptions GetContextOptions();
    }
}