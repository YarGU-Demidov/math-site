using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class RightConfiguration : AbstractEntityConfiguration<Right>
    {
        protected override string TableName { get; } = nameof(Right);

        /// <inheritdoc />
        protected override void SetKeys(EntityTypeBuilder<Right> modelBuilder)
        {
            modelBuilder
                .HasKey(right => right.Alias);
        }

        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<Right> modelBuilder)
        {
            modelBuilder
                .Property(right => right.Alias)
                .IsRequired();
            modelBuilder
                .Property(right => right.Description)
                .IsRequired(false);
            modelBuilder
                .Property(right => right.Name)
                .IsRequired();
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<Right> modelBuilder)
        {
            modelBuilder
                .HasMany(right => right.GroupsRights)
                .WithOne(groupsRights => groupsRights.Right)
                .HasForeignKey(groupsRights => groupsRights.RightAlias)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder
                .HasMany(right => right.UsersRights)
                .WithOne(usersRights => usersRights.Right)
                .HasForeignKey(usersRights => usersRights.RightAlias)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }

        protected override void SetIndexes(EntityTypeBuilder<Right> modelBuilder)
        {
        }
    }
}