using System;
using System.Security;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Db.DataSeeding;
using MathSite.Db.EntityConfiguration;
using MathSite.Domain.LogicValidation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MathSite.Tests.Domain
{
	public class TestDatabaseFactory: IDisposable
	{
		private static readonly IPasswordsManager PasswordsManager = new DoubleSha512HashPasswordsManager();

		private IMathSiteDbContext _context;
		private ILoggerFactory _loggerFactory;
		private SqliteConnection _connection;

		public IDisposable OpenConnection()
		{
			_connection = new SqliteConnection("DataSource=:memory:");
			_connection.Open();

			return _connection;
		}

		public IMathSiteDbContext GetContext()
		{
			
			_loggerFactory = new LoggerFactory();

			_context = new MathSiteDbContext(GetContextOptions(), new EntitiesConfigurator(_loggerFactory.CreateLogger<EntitiesConfigurator>()));

			if (!_context.Database.EnsureCreated())
			{
				_context.Database.Migrate();
			}

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

		public void Dispose()
		{
			_connection?.Close();
			_context?.Dispose();
		}

		public void ExecuteWithContext(Action<IMathSiteDbContext> yourAction)
		{
			using (OpenConnection())
			using (var context = GetContext())
			{
				yourAction(context);
			}
		}
	}
}