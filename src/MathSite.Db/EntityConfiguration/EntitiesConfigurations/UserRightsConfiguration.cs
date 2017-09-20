using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class UserRightsConfiguration : AbstractEntityConfiguration<UsersRight>
    {
        protected override string TableName { get; } = "UserRights";

        /// <inheritdoc />
        protected override void SetKeys(EntityTypeBuilder<UsersRight> modelBuilder)
        {
            modelBuilder
                .HasKey(gr => gr.Id);
        }

        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<UsersRight> modelBuilder)
        {
            modelBuilder
                .Property(gr => gr.Allowed)
                .IsRequired();
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<UsersRight> modelBuilder)
        {
            modelBuilder
                .HasOne(usersRights => usersRights.User)
                .WithMany(user => user.UserRights)
                .HasForeignKey(usersRights => usersRights.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .HasOne(usersRights => usersRights.Right)
                .WithMany(right => right.UsersRights)
                .HasForeignKey(usersRights => usersRights.RightAlias)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void SetIndexes(EntityTypeBuilder<UsersRight> modelBuilder)
        {
        }
    }
}