using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Common.Extensions;
using MathSite.Db;
using MathSite.Db.DataSeeding;
using MathSite.Entities;
using MathSite.Tests.CoreThings;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MathSite.Tests.Common
{
    internal class PersonModel
    {
        public string FullName { get; set; }

        public string Phone { get; set; }

        protected bool Equals(PersonModel other)
        {
            return string.Equals(FullName, other.FullName) && string.Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PersonModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((FullName != null ? FullName.GetHashCode() : 0) * 397) ^ (Phone != null ? Phone.GetHashCode() : 0);
            }
        }
    }

    public class ContextExtensionsTests
    {
        public ContextExtensionsTests()
        {
            _databaseFactory = TestSqliteDatabaseFactory.UseDefault();
        }

        private readonly ITestDatabaseFactory _databaseFactory;

        private void WithContext(Action<MathSiteDbContext> actions)
        {
            _databaseFactory.ExecuteWithContext(context =>
            {
                SeedData(context);
                actions(context);
            });
        }

        private async Task WithContextAsync(Func<MathSiteDbContext, Task> actions)
        {
            await _databaseFactory.ExecuteWithContextAsync(async context =>
            {
                SeedData(context);
                await actions(context);
            });
        }

        private static void SeedData(MathSiteDbContext context)
        {
            var seeder = new DataSeeder(
                context, 
                new LoggerFactory().AddDebug().CreateLogger<IDataSeeder>(),
                new DoubleSha512HashPasswordsManager()
            );

            seeder.Seed();
        }

        [Fact]
        public async Task FillingObject_Async_Success()
        {
            await WithContextAsync(async context =>
            {
                // sqlite3 syntax, other DBMSs may have a different one
                var value = await context.ExecuteSqlAsync<PersonModel>(
                    "SELECT (Surname || ' ' || Name) AS FullName FROM Person WHERE Surname = 'Тестов'"
                );

                Assert.Equal(new[] {new PersonModel {FullName = "Тестов Тест"}}, value.ToArray());
            });
        }

        [Fact]
        public async Task FillMoreComplex_Success()
        {
            await WithContextAsync(async context =>
            {
                await context.Persons.AddAsync(new Person {Name = "Test", Surname = "Test"});
                await context.SaveChangesAsync();

                // sqlite3 syntax, other DBMSs may have a different one
                var value = (await context.ExecuteSqlAsync<PersonModel>(
                    @"
                    SELECT 
                        (Surname || ' ' || Name) AS FullName, 
                        CASE WHEN Phone IS NOT NULL THEN 
                            Phone
                        END AS Phone
                    FROM 
                        Person 
                    WHERE 
                        Surname = 'Тестов' OR
                        Surname = 'Мокеев' OR
                        Surname = 'Test' AND Name = 'Test' AND Phone IS NULL
                    "
                )).OrderBy(p => $"{p.FullName}|{p.Phone}").ToArray();

                var expected = new[]
                {
                    new PersonModel {FullName = "Тестов Тест", Phone = "111111"},
                    new PersonModel {FullName = "Мокеев Андрей", Phone = "123456"},
                    new PersonModel {FullName = "Test Test", Phone = null}
                }.OrderBy(p => $"{p.FullName}|{p.Phone}").ToArray();

                Assert.Equal(expected, value);
            });
        }

        [Fact]
        public void FillingObject_Sync_Success()
        {
            WithContext(context =>
            {
                // sqlite3 syntax, other DBMSs may have a different one
                var value = context.ExecuteSql<PersonModel>(
                    "SELECT (Surname || ' ' || Name) AS FullName FROM Person WHERE Surname = 'Тестов'"
                );

                Assert.Equal(new[] {new PersonModel {FullName = "Тестов Тест"}}, value.ToArray());
            });
        }
    }
}