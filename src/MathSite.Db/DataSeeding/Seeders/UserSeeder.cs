using System;
using System.Collections.Generic;
using System.Linq;
using MathSite.Common.Crypto;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	using StaticData;

	/// <inheritdoc />
	public class UserSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public UserSeeder(ILogger logger, MathSiteDbContext context, IPasswordsManager passwordsManager) : base(logger,
			context)
		{
			PasswordManager = passwordsManager;
		}

		private IPasswordsManager PasswordManager { get; }

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "User";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.Users.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstLogin = "mokeev1995";
			var firstUser = CreateUser(
				firstLogin,
				GetPasswordHash(firstLogin, "test"),
				GetPersonByNames("Андрей", "Мокеев", "Александрович"),
				GetGroupByAlias(GroupAliases.Admin),
				DateTime.Now
			);

			var secondLogin = "andrey_devyatkin";
			var secondUser = CreateUser(
				secondLogin,
				GetPasswordHash(secondLogin, "qwerty"),
				GetPersonByNames("Андрей", "Девяткин", "Вячеславович"),
				GetGroupByAlias(GroupAliases.User),
				DateTime.Now
			);


			var users = new[]
			{
				firstUser,
				secondUser
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
				Settings = new List<UserSettings>(),
				PostsOwner = new List<PostOwner>(),
				AllowedPosts = new List<PostUserAllowed>(),
				PostsRatings = new List<PostRating>(),
				Comments = new List<Comment>(),
				UserRights = new List<UsersRights>()
			};
		}
	}
}