using System;
using System.Collections.Generic;
using System.Linq;
using MathSite.Common;
using MathSite.Common.Crypto;
using MathSite.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MathSite.Db
{
	public static class DbSeedData
	{
		public static void SeedData(this IServiceScopeFactory scopeFactory)
		{
			using (var serviceScope = scopeFactory.CreateScope())
			{
				using (var seeder = new DataSeeder(serviceScope.ServiceProvider.GetService<MathSiteDbContext>()))
				{
					seeder.SeedModelsData();
				}
			}
		}
	}

	internal class DataSeeder : IDisposable
	{
		private readonly MathSiteDbContext _dbContext;

		public DataSeeder(MathSiteDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Dispose()
		{
			_dbContext.SaveChanges();
		}

		public void SeedModelsData()
		{
			if (!_dbContext.Persons.Any())
				AddPersons();

			if (!_dbContext.Groups.Any())
				AddGroups();

			if (!_dbContext.Rights.Any())
				AddRights();

			if (!_dbContext.GroupsRights.Any())
				AddGroupsRightsRelations();

			if (!_dbContext.Users.Any())
			{
				AddUsers();

				SetUsersToGroups();
			}
		}

		private void AddPersons()
		{
			var mokeevAndreyPerson = new Person
			{
				Name = "Andrey",
				Surname = "Mokeev",
				MiddleName = "Aleksandrovich"
			};

			var testPerson = new Person
			{
				Name = "Test1",
				Surname = "Person",
				MiddleName = "Test3"
			};


			var persons = new[]
			{
				mokeevAndreyPerson,
				testPerson
			};

			_dbContext.Persons.AddRange(persons);
			Dispose();
		}

		private void AddUsers()
		{
			var adminGroup = _dbContext.Groups.First(group => group.Alias == "admin");
			var userGroup = _dbContext.Groups.First(group => group.Alias == "user");

			var mokeev1995 = new User
			{
				Login = "mokeev1995",
				PasswordHash = Passwords.GetHash("test"),
				Person =
					_dbContext.Persons.First(
						person => person.Name == "Andrey" && person.Surname == "Mokeev" && person.MiddleName == "Aleksandrovich"),
				Group = adminGroup
			};

			var testUser = new User
			{
				Login = "test",
				PasswordHash = Passwords.GetHash("test"),
				Person =
					_dbContext.Persons.First(
						person => person.Name == "Test1" && person.Surname == "Person" && person.MiddleName == "Test3"),
				Group = userGroup
			};


			var users = new[]
			{
				mokeev1995,
				testUser
			};

			_dbContext.Users.AddRange(users);
			Dispose();
		}

		private void AddGroups()
		{
			var administratorsGroup = new Group
			{
				Name = "Administrators",
				Description = "System Administrators",
				Alias = GroupsAliases.Admin,
				GroupsRights = new List<GroupsRights>()
			};

			var usersGroup = new Group
			{
				Name = "Site user",
				Description = "Simple site user",
				Alias = GroupsAliases.User,
				GroupsRights = new List<GroupsRights>()
			};

			var groups = new[]
			{
				administratorsGroup,
				usersGroup
			};
			_dbContext.Groups.AddRange(groups);
			Dispose();
		}

		private void AddRights()
		{
			var adminAccessRight = new Right
			{
				Name = "Admin Access",
				Description = "Allowing access to admin panel.",
				Alias = "admin",
				GroupsRights = new List<GroupsRights>()
			};

			var logoutRight = new Right
			{
				Name = "Logout Access",
				Description = "Allowing to logout",
				Alias = "logout",
				GroupsRights = new List<GroupsRights>()
			};

			var panelAccessRight = new Right
			{
				Name = "Panel Access",
				Description = "Allowing to access user panel",
				Alias = "panel",
				GroupsRights = new List<GroupsRights>()
			};

			var rights = new[]
			{
				adminAccessRight,
				logoutRight,
				panelAccessRight
			};

			_dbContext.Rights.AddRange(rights);
			Dispose();
		}

		private void AddGroupsRightsRelations()
		{
			var adminGroup = _dbContext.Groups.First(group => group.Alias == "admin");
			var usersGroup = _dbContext.Groups.First(group => group.Alias == "user");

			var adminAccessRight = _dbContext.Rights.First(right => right.Alias == "admin");
			var logoutAccess = _dbContext.Rights.First(right => right.Alias == "logout");
			var panelAccess = _dbContext.Rights.First(right => right.Alias == "panel");

			var adminRights = new[]
			{
				new GroupsRights {Allowed = true, Group = adminGroup, Right = adminAccessRight},
				new GroupsRights {Allowed = true, Group = adminGroup, Right = logoutAccess},
				new GroupsRights {Allowed = true, Group = adminGroup, Right = panelAccess}
			};

			var usersRights = new[]
			{
				new GroupsRights {Allowed = false, Group = usersGroup, Right = adminAccessRight},
				new GroupsRights {Allowed = true, Group = usersGroup, Right = logoutAccess},
				new GroupsRights {Allowed = true, Group = usersGroup, Right = panelAccess}
			};

			_dbContext.GroupsRights.AddRange(usersRights);
			Dispose();

			_dbContext.GroupsRights.AddRange(adminRights);
			Dispose();
		}

		private void SetUsersToGroups()
		{
			var adminGroup = _dbContext.Groups.First(group => group.Alias == "admin");
			var usersGroup = _dbContext.Groups.First(group => group.Alias == "user");

			var mokeev1995 = _dbContext.Users.First(user => user.Login == "mokeev1995");
			var testUser = _dbContext.Users.First(user => user.Login == "test");

			mokeev1995.Group = adminGroup;
			testUser.Group = usersGroup;

			Dispose();
		}
	}
}