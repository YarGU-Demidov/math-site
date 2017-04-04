using System.Linq;
using MathSite.Common.Crypto;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class UsersSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public UsersSeeder(ILogger logger, MathSiteDbContext context, IPasswordHasher passwordHasher) : base(logger, context)
		{
			PasswordHasher = passwordHasher;
		}

		private IPasswordHasher PasswordHasher { get; }

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "Users";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.Users.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var mokeev1995 = CreateUser(
				"mokeev1995",
				GetPasswordHash("test"),
				GetPersonByNames("Андрей", "Мокеев", "Александрович"),
				GetGroupByAlias(GroupsAliases.Admin)
			);

			var test = CreateUser(
				"test",
				GetPasswordHash("test"),
				GetPersonByNames("Test1", "Person", "Test3"),
				GetGroupByAlias(GroupsAliases.User)
			);


			var users = new[]
			{
				mokeev1995,
				test
			};

			Context.Users.AddRange(users);
		}

		private User CreateUser(string login, string password, Person person, Group group)
		{
			return new User
			{
				Login = login,
				PasswordHash = password,
				Person = person,
				Group = group
			};
		}

		private Group GetGroupByAlias(string alias)
		{
			return Context.Groups.First(group => group.Alias == alias);
		}

		private string GetPasswordHash(string password)
		{
			return PasswordHasher.GetHash(password);
		}

		private Person GetPersonByNames(string name, string surname, string middlename)
		{
			return Context.Persons.First(
				person => person.Name == name &&
				          person.Surname == surname &&
				          person.MiddleName == middlename
			);
		}
	}
}