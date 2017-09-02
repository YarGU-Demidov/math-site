using System;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class PersonSeeder : AbstractSeeder<Person>
	{
		/// <inheritdoc />
		public PersonSeeder(ILogger logger, MathSiteDbContext context)
			: base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(Person);

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

			var thirdPerson = CreatePerson(
				"Тест",
				"Тестов",
				"Тестович",
				DateTime.Now - TimeSpan.FromDays(365),
				"111111",
				"222222"
			);

			var persons = new[]
			{
				firstPerson,
				secondPerson,
				thirdPerson
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