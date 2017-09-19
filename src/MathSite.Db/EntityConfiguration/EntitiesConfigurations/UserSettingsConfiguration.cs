using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class UserSettingsConfiguration : AbstractEntityConfiguration<UserSetting>
    {
        protected override string TableName { get; } = nameof(UserSetting);

        /// <inheritdoc />
        protected override void SetKeys(EntityTypeBuilder<UserSetting> modelBuilder)
        {
            modelBuilder
                .HasKey(userSettings => userSettings.Id);
        }

        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<UserSetting> modelBuilder)
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
        protected override void SetRelationships(EntityTypeBuilder<UserSetting> modelBuilder)
        {
            modelBuilder
                .HasOne(userSettings => userSettings.User)
                .WithMany(user => user.Settings)
                .HasForeignKey(userSettings => userSettings.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void SetIndexes(EntityTypeBuilder<UserSetting> modelBuilder)
        {
        }
    }
}