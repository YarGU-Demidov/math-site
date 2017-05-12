using System.Collections.Generic;
using MathSite.Common.Crypto;
using MathSite.Db.DataSeeding.Seeders;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding
{
	/// <inheritdoc />
	public class DataSeeder : IDataSeeder
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

		private IEnumerable<ISeeder> GetSeeders()
		{
			return new List<ISeeder>
			{
				new GroupTypeSeeder(_logger, _context),
				new GroupSeeder(_logger, _context),
				new PersonSeeder(_logger, _context),
				new FileSeeder(_logger, _context),
				new UserSeeder(_logger, _context, _passwordHasher),
				new RightSeeder(_logger, _context),
				new GroupRightsSeeder(_logger, _context),
				new PostTypeSeeder(_logger, _context),
				new PostSettingsSeeder(_logger, _context),
				new PostSeoSettingsSeeder(_logger, _context),
				new PostSeeder(_logger, _context),
				new CommentSeeder(_logger, _context),
				new PostGroupsAllowedSeeder(_logger, _context),
				new PostRatingSeeder(_logger, _context),
				new PostUserAllowedSeeder(_logger, _context),
				new PostOwnerSeeder(_logger, _context),
				new UsersToGroupsSeeder(_logger, _context),
				new UserRightsSeeder(_logger, _context),
				new UserSettingsSeeder(_logger, _context),
				new CategorySeeder(_logger, _context),
				new PostCategorySeeder(_logger, _context),
				new PostAttachmentSeeder(_logger, _context),
				new KeywordSeeder(_logger, _context),
				new PostKeywordsSeeder(_logger, _context)
			};
		}
	}
}