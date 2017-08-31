using System.Collections.Generic;
using MathSite.Common.Crypto;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace MathSite.Tests.Domain
{
	public class TestSqliteDatabaseFactory : TestDatabaseFactory<SqliteConnection>
	{
		public TestSqliteDatabaseFactory(SqliteConnection connection, IPasswordsManager passwordsManager,
			ILoggerFactory loggerFactory)
			: base(connection, passwordsManager, loggerFactory)
		{
		}

		public static TestSqliteDatabaseFactory UseDefault(SqliteConnection connection = null,
			IPasswordsManager passwordsManager = null, ILoggerFactory loggerFactory = null)
		{
			if (connection == null)
				connection = new SqliteConnection("DataSource=:memory:");

			if (passwordsManager == null)
				passwordsManager = new DoubleSha512HashPasswordsManager();

			if (loggerFactory == null)
				loggerFactory = new LoggerFactory(new List<ILoggerProvider> {new DebugLoggerProvider()});

			return new TestSqliteDatabaseFactory(connection, passwordsManager, loggerFactory);
		}
	}
}