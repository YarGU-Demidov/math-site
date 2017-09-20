using System;
using System.Data.Common;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Db.DataSeeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MathSite.Tests.CoreThings
{
    public abstract class TestDatabaseFactory : ITestDatabaseFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IPasswordsManager _passwordsManager;
        protected readonly DbConnection Connection;

        private MathSiteDbContext _context;

        public TestDatabaseFactory(DbConnection connection, IPasswordsManager passwordsManager,
            ILoggerFactory loggerFactory)
        {
            Connection = connection;
            _passwordsManager = passwordsManager;
            _loggerFactory = loggerFactory;
        }

        public TestDatabaseFactory(IPasswordsManager passwordsManager, ILoggerFactory loggerFactory)
            : this(null, passwordsManager, loggerFactory)
        {
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
            _context = new MathSiteDbContext(GetContextOptions());

            await _context.Database.EnsureCreatedAsync();
            await _context.Database.MigrateAsync();

            SeedData();

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

        private void SeedData()
        {
            var dataSeederLogger = _loggerFactory.CreateLogger<IDataSeeder>();

            var seeder = new DataSeeder(_context, dataSeederLogger, _passwordsManager);

            seeder.Seed();
        }
    }
}