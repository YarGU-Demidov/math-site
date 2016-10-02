using System;
using System.Collections.Generic;
using System.Linq;
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
				var context = serviceScope.ServiceProvider.GetService<MathSiteDbContext>();

				#region Add persons

				var mokeevAndreyPerson = new Person
				{
					Id = Guid.NewGuid(),
					Name = "Andrey",
					Surname = "Mokeev",
					MiddleName = "Aleksandrovich"
				};

				#endregion

				#region Add users

				var mokeev1995 = new User
				{
					Id = Guid.NewGuid(),
					Login = "mokeev1995",
					PasswordHash = Passwords.GetHash("test"),
					Person = mokeevAndreyPerson,
					PersonId = mokeevAndreyPerson.Id
				};

				#endregion

				#region Add groups

				var administratorsGroup = new Group
				{
					Id = Guid.NewGuid(),
					Name = "Administrators",
					Description = "System Administrators",
					Alias = "admin"
				};

				#endregion

				#region Add rights

				var adminAccessRight = new Right
				{
					Id = Guid.NewGuid(),
					Name = "Admin Access",
					Description = "Allowing access to admin panel."
				};

				#endregion

				#region Add relations groups-rights

				var adminGroupRight = new GroupsRights()
				{
					Id = Guid.NewGuid(),
					Group = administratorsGroup,
					GroupId = administratorsGroup.Id,
					Right = adminAccessRight,
					RightId = adminAccessRight.Id,
					Allowed = true
				};

				var groupsRights = new List<GroupsRights> { adminGroupRight };

				administratorsGroup.GroupsRights = groupsRights;
				adminAccessRight.GroupsRights = groupsRights;

				mokeev1995.Group = administratorsGroup;
				mokeev1995.GroupId = administratorsGroup.Id;

				#endregion

				if (!context.Persons.Any() && !context.Users.Any() && !context.Groups.Any())
				{
					context.Persons.Add(mokeevAndreyPerson);
					context.Persons.Add(new Person {Id = Guid.NewGuid(), Name = "Test1", Surname = "Test2", MiddleName = "Test3"});
					context.Users.Add(mokeev1995);

					context.Groups.Add(administratorsGroup);
					context.Rights.Add(adminAccessRight);

					context.SaveChanges();
				}
			}
		}
	}
}