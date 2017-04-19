using System;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class PersonSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public PersonSeeder(ILogger logger, MathSiteDbContext context)
			: base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "Person";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.Persons.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPerson = CreatePerson(
				"Андрей",
				"Мокеев",
				"Александрович",
				DateTime.Now,
				"123456",
				"654321",
				GetUserByGroup("Students"),
				GetFileByAlias(FileAliases.FirstFile)
			);

			var secondPerson = CreatePerson(
				"Андрей",
				"Девяткин",
				"Вячеславович",
				DateTime.Now,
				"234567",
				"765432",
				GetUserByGroup("Employees"),
				GetFileByAlias(FileAliases.SecondFile)
			);

			var persons = new[]
			{
				firstPerson,
				secondPerson
			};

			Context.Persons.AddRange(persons);
		}

		private User GetUserByGroup(string name)
		{
			return Context.Users.FirstOrDefault(u => u.Group.Name == name);
		}

		private File GetFileByAlias(string alias)
		{
			return Context.Files.First(u => u.FileName == alias);
		}

		private static Person CreatePerson(string name, string surname, string middlename, DateTime birthday, string phone,
			string additionalPhone, User user, File photo)
		{
			return new Person
			{
				Name = name,
				Surname = surname,
				MiddleName = middlename,
				Birthday = birthday,
				Phone = phone,
				AdditionalPhone = additionalPhone,
				User = user,
				Photo = photo,
				CreationDate = DateTime.Now
			};
		}
	}
}