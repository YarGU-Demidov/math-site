using System.Linq;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class PersonsSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public PersonsSeeder(ILogger logger, MathSiteDbContext context)
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
			var mokeevAndreyPerson = CreatePerson("Андрей", "Мокеев", "Александрович");

			var testPerson = CreatePerson("Test1", "Person", "Test3");

			var persons = new[]
			{
				mokeevAndreyPerson,
				testPerson
			};

			Context.Persons.AddRange(persons);
		}

		private static Person CreatePerson(string name, string surname, string middlename)
		{
			return new Person
			{
				Name = name,
				Surname = surname,
				MiddleName = middlename
			};
		}
	}
}