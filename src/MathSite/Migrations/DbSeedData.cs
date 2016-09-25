using System;
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

				var user1Guid = Guid.NewGuid();
				var user = new User {Id = user1Guid, Login = "mokeev1995", PasswordHash = Passwords.GetHash("test")};
				var person = new Person
				{
					Id = Guid.NewGuid(),
					Name = "Andrey",
					Surname = "Mokeev",
					MiddleName = "Aleksandrovich",
					UserId = user1Guid,
					User = user
				};

				user.Person = person;


				if (!context.Persons.Any() && !context.Users.Any())
				{
					context.Persons.Add(person);
					context.Persons.Add(new Person {Id = Guid.NewGuid(), Name = "Test1", Surname = "Test2", MiddleName = "Test3"});
					context.Users.Add(user);

					context.SaveChanges();
				}
					
			}
		}
	}
}