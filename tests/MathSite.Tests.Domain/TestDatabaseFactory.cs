using System;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Db.DataSeeding;
using MathSite.Db.EntityConfiguration;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MathSite.Tests.Domain
{
	public class TestDatabaseFactory : IDisposable
	{
		private static readonly IPasswordsManager PasswordsManager = new DoubleSha512HashPasswordsManager();
		private SqliteConnection _connection;

		private IMathSiteDbContext _context;
		private ILoggerFactory _loggerFactory;

		public void Dispose()
		{
			_connection?.Close();
			_context?.Dispose();
		}

		public IDisposable OpenConnection()
		{
			_connection = new SqliteConnection("DataSource=:memory:");
			_connection.Open();

			return _connection;
		}

		public IMathSiteDbContext GetContext()
		{
			_loggerFactory = new LoggerFactory();

			_context = new MathSiteDbContext(
				GetContextOptions(),
				new EntitiesConfigurator(_loggerFactory.CreateLogger<EntitiesConfigurator>())
			);

			if (!_context.Database.EnsureCreated())
				_context.Database.Migrate();

			SeedData();

			return _context;
		}

		private DbContextOptions GetContextOptions()
		{
			return new DbContextOptionsBuilder()
				.UseSqlite(_connection)
				.Options;
		}

		private void SeedData()
		{
			var dataSeederLogger = _loggerFactory.CreateLogger<DataSeeder>();

			var seeder = new DataSeeder(_context, dataSeederLogger, PasswordsManager);

			seeder.Seed();
		}

		public void ExecuteWithContext(Action<IMathSiteDbContext> yourAction)
		{
			using (OpenConnection())
			using (var context = GetContext())
			{
				yourAction(context);
			}
		}

		public async Task ExecuteWithContextAsync(Func<IMathSiteDbContext, Task> yourAction)
		{
			using (OpenConnection())
			using (var context = GetContext())
			{
				await yourAction(context);
			}
		}
	}
}