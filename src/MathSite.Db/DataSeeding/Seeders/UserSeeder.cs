using System;
using System.Collections.Generic;
using System.Linq;
using MathSite.Common.Crypto;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class UserSeeder : AbstractSeeder<User>
	{
		/// <inheritdoc />
		public UserSeeder(ILogger logger, MathSiteDbContext context, IPasswordsManager passwordsManager) : base(logger,
			context)
		{
			PasswordManager = passwordsManager;
		}

		private IPasswordsManager PasswordManager { get; }

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(User);

		/// <inheritdoc />
		protected override void SeedData()
		{
			const string firstLogin = UsersAliases.FirstUser;
			var firstUser = CreateUser(
				firstLogin,
				GetPasswordHash(firstLogin, "test"),
				GetPersonByNames("Андрей", "Мокеев", "Александрович"),
				GetGroupByAlias(GroupAliases.Admin),
				DateTime.Now
			);

			const string secondLogin = UsersAliases.SecondUser;
			var secondUser = CreateUser(
				secondLogin,
				GetPasswordHash(secondLogin, "qwerty"),
				GetPersonByNames("Андрей", "Девяткин", "Вячеславович"),
				GetGroupByAlias(GroupAliases.User),
				DateTime.Now
			);

			const string testLogin = UsersAliases.TestUser;
			var testUser = CreateUser(
				testLogin,
				GetPasswordHash(testLogin, "test"),
				GetPersonByNames("Тест", "Тестов", "Тестович"),
				GetGroupByAlias(GroupAliases.User),
				DateTime.Now
			);


			var users = new[]
			{
				firstUser,
				secondUser,
				testUser
			};

			Context.Users.AddRange(users);
		}

		private Group GetGroupByAlias(string alias)
		{
			return Context.Groups.First(group => group.Alias == alias);
		}

		private byte[] GetPasswordHash(string login, string password)
		{
			return PasswordManager.CreatePassword(login, password);
		}

		private Person GetPersonByNames(string name, string surname, string middlename)
		{
			return Context.Persons.First(
				person => person.Name == name &&
				          person.Surname == surname &&
				          person.MiddleName == middlename
			);
		}

		private static User CreateUser(string login, byte[] password, Person person, Group group, DateTime creationDate)
		{
			return new User
			{
				Login = login,
				PasswordHash = password,
				Person = person,
				Group = group,
				CreationDate = creationDate,
				Settings = new List<UserSetting>(),
				PostsOwner = new List<PostOwner>(),
				AllowedPosts = new List<PostUserAllowed>(),
				PostsRatings = new List<PostRating>(),
				Comments = new List<Comment>(),
				UserRights = new List<UsersRight>()
			};
		}
	}
}