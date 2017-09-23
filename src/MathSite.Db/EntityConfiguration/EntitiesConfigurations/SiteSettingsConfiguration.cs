using MathSite.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class SiteSettingsConfiguration : AbstractEntityConfiguration<SiteSetting>
    {
        protected override string TableName { get; } = nameof(SiteSetting);

        protected override void SetKeys(EntityTypeBuilder<SiteSetting> modelBuilder)
        {
            base.SetKeys(modelBuilder);

            modelBuilder.HasAlternateKey(settings => settings.Key);
        }

        protected override void SetFields(EntityTypeBuilder<SiteSetting> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder.Property(settings => settings.Key)
                .IsRequired();
            modelBuilder.Property(settings => settings.Value)
                .IsRequired();
        }

        protected override void SetIndexes(EntityTypeBuilder<SiteSetting> modelBuilder)
        {
            base.SetIndexes(modelBuilder);

            modelBuilder.HasIndex(settings => settings.Key)
                .IsUnique();
        }
    }
}