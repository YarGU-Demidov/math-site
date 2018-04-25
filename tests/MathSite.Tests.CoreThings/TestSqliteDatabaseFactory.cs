using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Tests.CoreThings
{
    public class TestSqliteDatabaseFactory : TestDatabaseFactory
    {
        protected override bool IgnoreSQLiteWrongData { get; } = true;

        public TestSqliteDatabaseFactory(SqliteConnection connection)
            : base(connection)
        {
        }

        public static TestSqliteDatabaseFactory UseDefault(SqliteConnection connection = null)
        {
            if (connection == null)
                connection = new SqliteConnection("DataSource=:memory:");

            return new TestSqliteDatabaseFactory(connection);
        }

        protected override DbContextOptions GetContextOptions()
        {
            return new DbContextOptionsBuilder()
                .UseSqlite(Connection)
                .Options;
        }
    }
}