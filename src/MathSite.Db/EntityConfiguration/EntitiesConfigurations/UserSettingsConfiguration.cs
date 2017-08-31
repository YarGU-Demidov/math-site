using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class UserSettingsConfiguration : AbstractEntityConfiguration<UserSettings>
	{
		/// <inheritdoc />
		protected override void SetKeys(EntityTypeBuilder<UserSettings> modelBuilder)
		{
			modelBuilder
				.HasKey(userSettings => userSettings.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<UserSettings> modelBuilder)
		{
			modelBuilder
				.Property(userSettings => userSettings.Namespace)
				.IsRequired();

			modelBuilder
				.Property(userSettings => userSettings.Key)
				.IsRequired();

			modelBuilder
				.Property(userSettings => userSettings.Value)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<UserSettings> modelBuilder)
		{
			modelBuilder
				.HasOne(userSettings => userSettings.User)
				.WithMany(user => user.Settings)
				.HasForeignKey(userSettings => userSettings.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}