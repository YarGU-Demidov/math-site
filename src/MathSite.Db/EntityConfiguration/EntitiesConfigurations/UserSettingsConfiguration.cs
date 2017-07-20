using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class UserSettingsConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserSettings>()
				.HasKey(userSettings => userSettings.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserSettings>()
				.Property(userSettings => userSettings.Namespace)
				.IsRequired();

			modelBuilder.Entity<UserSettings>()
				.Property(userSettings => userSettings.Key)
				.IsRequired();

			modelBuilder.Entity<UserSettings>()
				.Property(userSettings => userSettings.Value)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserSettings>()
				.HasOne(userSettings => userSettings.User)
				.WithMany(user => user.Settings)
				.HasForeignKey(userSettings => userSettings.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "UserSettings";
	}
}