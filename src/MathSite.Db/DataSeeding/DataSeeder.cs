using System.Collections.Generic;
using MathSite.Common.Crypto;
using MathSite.Db.DataSeeding.Seeders;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding
{
	/// <inheritdoc />
	public class DataSeeder: IDataSeeder
	{
		private readonly MathSiteDbContext _context;
		private readonly ILogger _logger;
		private readonly IPasswordsManager _passwordHasher;

		///  <summary>
		/// 		Создается Data Seeder
		///  </summary>
		///  <param name="context">Контекст базы сайта</param>
		///  <param name="logger">Логгер</param>
		/// <param name="passwordHasher">Парольный хэшировщик</param>
		public DataSeeder(MathSiteDbContext context, ILogger<DataSeeder> logger, IPasswordsManager passwordHasher)
		{
			_context = context;
			_logger = logger;
			_passwordHasher = passwordHasher;
		}

		/// <inheritdoc />
		public void Seed()
		{
			var seeders = GetSeeders();

			_logger.LogInformation("Trying to seed data.");
			foreach (var seeder in seeders)
			{
				if (seeder.CanSeed)
				{
					_logger.LogInformation($"Trying seed {seeder.SeedingObjectName}");
					using (seeder)
					{
						seeder.Seed();
					}
					_logger.LogInformation($"Seeding {seeder.SeedingObjectName} complete!");
				}
				else
				{
					_logger.LogInformation($"Seeding {seeder.SeedingObjectName} skipped!");
				}
			}
			_logger.LogInformation("Seeding Done! Continue start server...");
		}

		// тут происходит магия сидирования всех данных, порядок создания ВАЖЕН! ОЧЕНЬ!
		private IEnumerable<ISeeder> GetSeeders()
		{
			return new List<ISeeder>
			{
				new PersonsSeeder(_logger, _context),
				new GroupsSeeder(_logger, _context),
				new RightsSeeder(_logger, _context),
				new GroupRightsRelationSeeder(_logger, _context),
				new UsersSeeder(_logger, _context, _passwordHasher),
				new UsersToGroupsSeeder(_logger, _context)
			};
		}
	}
}