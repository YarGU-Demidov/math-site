using System;
using System.Linq;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class PersonSeeder : AbstractSeeder<Person>
	{
		/// <inheritdoc />
		public PersonSeeder(ILogger logger, IMathSiteDbContext context)
			: base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "Person";
		
		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPerson = CreatePerson(
				"Андрей",
				"Мокеев",
				"Александрович",
				DateTime.Now,
				"123456",
				"654321"
			);

			var secondPerson = CreatePerson(
				"Андрей",
				"Девяткин",
				"Вячеславович",
				DateTime.Now,
				"234567",
				"765432"
			);

			var persons = new[]
			{
				firstPerson,
				secondPerson
			};

			Context.Persons.AddRange(persons);
		}

		private static Person CreatePerson(string name, string surname, string middlename, DateTime birthday, string phone,
			string additionalPhone)
		{
			return new Person
			{
				Name = name,
				Surname = surname,
				MiddleName = middlename,
				Birthday = birthday,
				Phone = phone,
				AdditionalPhone = additionalPhone,
				CreationDate = DateTime.Now
			};
		}
	}
}