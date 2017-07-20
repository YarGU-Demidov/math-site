using MathSite.Db.EntityConfiguration;
using MathSite.Db.EntityConfiguration.EntitiesConfigurations;
using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace MathSite.Db
{
	/// <summary>
	///     Контекст сайта.
	/// </summary>
	public class MathSiteDbContext : DbContext, IMathSiteDbContext
	{
		private readonly IEntitiesConfigurator _configurator;
		private readonly ILoggerFactory _loggerFactory;

		/// <summary>
		///     Контекст.
		/// </summary>
		/// <param name="options">Настройки контекста.</param>
		/// <param name="configurator">Конфигуратор моделей.</param>
		/// <param name="loggerFactory">Фабрика логгеров</param>
		public MathSiteDbContext(DbContextOptions options, IEntitiesConfigurator configurator,
			ILoggerFactory loggerFactory) : base(options)
		{
			_configurator = configurator;
			_loggerFactory = loggerFactory;
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<File> Files { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<GroupsRights> GroupsRights { get; set; }
		public DbSet<GroupType> GroupTypes { get; set; }
		public DbSet<Keywords> Keywords { get; set; }
		public DbSet<Person> Persons { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<PostAttachment> PostAttachments { get; set; }
		public DbSet<PostCategory> PostCategories { get; set; }
		public DbSet<PostGroupsAllowed> PostGroupsAlloweds { get; set; }
		public DbSet<PostKeywords> PostKeywords { get; set; }
		public DbSet<PostOwner> PostOwners { get; set; }
		public DbSet<PostRating> PostRatings { get; set; }
		public DbSet<PostSeoSettings> PostSeoSettings { get; set; }
		public DbSet<PostSettings> PostSettings { get; set; }
		public DbSet<PostType> PostTypes { get; set; }
		public DbSet<PostUserAllowed> PostUserAlloweds { get; set; }
		public DbSet<Right> Rights { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserSettings> UserSettingses { get; set; }
		public DbSet<UsersRights> UsersRights { get; set; }

		/// <summary>
		///     Добавление конфигурации сущностей.
		/// </summary>
		/// <param name="modelBuilder">
		///     <inheritdoc />
		/// </param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			_configurator.AddConfiguration(new CategoryConfiguration());
			_configurator.AddConfiguration(new CommentConfiguration());
			_configurator.AddConfiguration(new FileConfiguration());
			_configurator.AddConfiguration(new GroupRightsConfiguration());
			_configurator.AddConfiguration(new GroupTypeConfiguration());
			_configurator.AddConfiguration(new KeywordsConfiguration());
			_configurator.AddConfiguration(new PersonConfiguration());
			_configurator.AddConfiguration(new PostAttachmentConfiguration());
			_configurator.AddConfiguration(new PostCategoryConfiguration());
			_configurator.AddConfiguration(new PostConfiguration());
			_configurator.AddConfiguration(new PostGroupsAllowedConfiguration());
			_configurator.AddConfiguration(new PostKeywordsConfiguration());
			_configurator.AddConfiguration(new PostOwnerConfiguration());
			_configurator.AddConfiguration(new PostRatingConfiguration());
			_configurator.AddConfiguration(new PostSeoSettingsConfiguration());
			_configurator.AddConfiguration(new PostSettingsConfiguration());
			_configurator.AddConfiguration(new PostTypeConfiguration());
			_configurator.AddConfiguration(new PostUserAllowedConfiguration());
			_configurator.AddConfiguration(new RightConfiguration());
			_configurator.AddConfiguration(new UserRightsConfiguration());
			_configurator.AddConfiguration(new UserConfiguration());
			_configurator.AddConfiguration(new UserSettingsConfiguration());

			_configurator.Configure(modelBuilder);

			modelBuilder.HasPostgresExtension("uuid-ossp");
			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseLoggerFactory(_loggerFactory);
		}
	}
}