using MathSite.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class SiteSettingsConfiguration : AbstractEntityConfiguration<SiteSettings>
	{
		protected override string TableName { get; } = nameof(SiteSettings);

		protected override void SetKeys(EntityTypeBuilder<SiteSettings> modelBuilder)
		{
			modelBuilder.HasKey(settings => settings.Key);
		}

		protected override void SetFields(EntityTypeBuilder<SiteSettings> modelBuilder)
		{
			modelBuilder.Property(settings => settings.Key)
				.IsRequired();
			modelBuilder.Property(settings => settings.Value)
				.IsRequired();
		}

		protected override void SetRelationships(EntityTypeBuilder<SiteSettings> modelBuilder)
		{
		}

		protected override void SetIndexes(EntityTypeBuilder<SiteSettings> modelBuilder)
		{
			modelBuilder.HasIndex(settings => settings.Key);
		}
	}
}