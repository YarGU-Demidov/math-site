using MathSite.Db.EntityConfiguration;
using MathSite.Db.EntityConfiguration.EntitiesConfigurations;
using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace MathSite.Db
{
	// ReSharper disable once ClassNeverInstantiated.Global
	/// <summary>
	///		Контекст сайта
	/// </summary>
	public class MathSiteDbContext : DbContext
	{
		private readonly IEntitiesConfigurator _configurator;

		/// <summary>
		///		Контекст
		/// </summary>
		/// <param name="options"><inheritdoc /></param>
		/// <param name="configurator">Конфигуратор моделей</param>
		public MathSiteDbContext(DbContextOptions options, IEntitiesConfigurator configurator) : base(options)
		{
			_configurator = configurator;
		}

		public DbSet<Person> Persons { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Right> Rights { get; set; }
		public DbSet<GroupsRights> GroupsRights { get; set; }
		public DbSet<UsersRights> UsersRights { get; set; }

		/// <summary>
		///     Настраиваем сущности: <br />
		///     устанавливаем все ключи (первичные, внешние), связи между сущностями
		/// </summary>
		/// <param name="modelBuilder"><inheritdoc /></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			_configurator.AddConfiguration(new UsersConfiguration());
			_configurator.AddConfiguration(new PersonsConfiguration());
			_configurator.AddConfiguration(new RightsConfiguration());
			_configurator.AddConfiguration(new GroupsConfiguration());
			_configurator.AddConfiguration(new GroupRightsConfiguration());
			_configurator.AddConfiguration(new UserRightsConfiguration());
			
			_configurator.Configure(modelBuilder);

			modelBuilder.HasPostgresExtension("uuid-ossp");
			base.OnModelCreating(modelBuilder);
		}
	}
}