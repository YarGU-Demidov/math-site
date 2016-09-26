using System;
using System.Collections.Generic;
using System.Linq;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MathSite.Migrations
{
	public static class DbSeedData
	{
		public static void SeedData(this IServiceScopeFactory scopeFactory)
		{
			using (var serviceScope = scopeFactory.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<MathSiteDbContext>();

				var user = new User {Id = Guid.NewGuid(), Login = "mokeev1995", PasswordHash = Passwords.GetHash("test")};
				var person = new Person
				{
					Id = Guid.NewGuid(),
					Name = "Andrey",
					Surname = "Mokeev",
					MiddleName = "Aleksandrovich",
					UserId = user.Id,
					User = user
				};
				user.Person = person;

				var administratorsGroup = new Group
				{
					Id = Guid.NewGuid(),
					Name = "Administrators",
					Description = "System Administrators",
					Alias = "admin"
				};

				var adminAccessRight = new Right {Name = "Admin Access", Description = "Allowing access to admin panel."};

				var adminGroupRight = new GroupsRights()
				{
					Id = Guid.NewGuid(),
					Group = administratorsGroup,
					GroupId = administratorsGroup.Id,
					Right = adminAccessRight,
					RightId = adminAccessRight.Id
				};

				var groupsRights = new List<GroupsRights> {adminGroupRight};

				administratorsGroup.GroupsRights = groupsRights;
				adminAccessRight.GroupsRights = groupsRights;

				user.Group = administratorsGroup;
				user.GroupId = administratorsGroup.Id;

				if (!context.Persons.Any() && !context.Users.Any() && !context.Groups.Any())
				{
					context.Persons.Add(person);
					context.Persons.Add(new Person {Id = Guid.NewGuid(), Name = "Test1", Surname = "Test2", MiddleName = "Test3"});
					context.Users.Add(user);

					context.Groups.Add(administratorsGroup);
					context.Rights.Add(adminAccessRight);

					context.SaveChanges();
				}
			}
		}
	}
}