using MathSite.Db.EntityConfiguration.EntitiesConfigurations;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Db
{
	/// <summary>
	///     Контекст сайта.
	/// </summary>
	public class MathSiteDbContext : DbContext
	{
		/// <summary>
		///     Контекст.
		/// </summary>
		/// <param name="options">Настройки контекста.</param>
		public MathSiteDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<File> Files { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<GroupsRight> GroupsRights { get; set; }
		public DbSet<GroupType> GroupTypes { get; set; }
		public DbSet<Keyword> Keywords { get; set; }
		public DbSet<Person> Persons { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<PostAttachment> PostAttachments { get; set; }
		public DbSet<PostCategory> PostCategories { get; set; }
		public DbSet<PostGroupsAllowed> PostGroupsAlloweds { get; set; }
		public DbSet<PostKeyword> PostKeywords { get; set; }
		public DbSet<PostOwner> PostOwners { get; set; }
		public DbSet<PostRating> PostRatings { get; set; }
		public DbSet<PostSeoSetting> PostSeoSettings { get; set; }
		public DbSet<PostSetting> PostSettings { get; set; }
		public DbSet<PostType> PostTypes { get; set; }
		public DbSet<PostUserAllowed> PostUserAlloweds { get; set; }
		public DbSet<Right> Rights { get; set; }
		public DbSet<SiteSetting> SiteSettings { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserSetting> UserSettingses { get; set; }
		public DbSet<UsersRight> UsersRights { get; set; }

		/// <summary>
		///     Добавление конфигурации сущностей.
		/// </summary>
		/// <param name="modelBuilder">
		///     <inheritdoc />
		/// </param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());
			modelBuilder.ApplyConfiguration(new CommentConfiguration());
			modelBuilder.ApplyConfiguration(new FileConfiguration());
			modelBuilder.ApplyConfiguration(new GroupRightsConfiguration());
			modelBuilder.ApplyConfiguration(new GroupTypeConfiguration());
			modelBuilder.ApplyConfiguration(new KeywordsConfiguration());
			modelBuilder.ApplyConfiguration(new GroupConfiguration());
			modelBuilder.ApplyConfiguration(new PersonConfiguration());
			modelBuilder.ApplyConfiguration(new PostAttachmentConfiguration());
			modelBuilder.ApplyConfiguration(new PostCategoryConfiguration());
			modelBuilder.ApplyConfiguration(new PostConfiguration());
			modelBuilder.ApplyConfiguration(new PostGroupsAllowedConfiguration());
			modelBuilder.ApplyConfiguration(new PostKeywordsConfiguration());
			modelBuilder.ApplyConfiguration(new PostOwnerConfiguration());
			modelBuilder.ApplyConfiguration(new PostRatingConfiguration());
			modelBuilder.ApplyConfiguration(new PostSeoSettingsConfiguration());
			modelBuilder.ApplyConfiguration(new PostSettingsConfiguration());
			modelBuilder.ApplyConfiguration(new PostTypeConfiguration());
			modelBuilder.ApplyConfiguration(new PostUserAllowedConfiguration());
			modelBuilder.ApplyConfiguration(new RightConfiguration());
			modelBuilder.ApplyConfiguration(new SiteSettingsConfiguration());
			modelBuilder.ApplyConfiguration(new UserRightsConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new UserSettingsConfiguration());

			modelBuilder.HasPostgresExtension("uuid-ossp");
			base.OnModelCreating(modelBuilder);
		}
	}
}