using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class GroupRightsConfiguration : AbstractEntityConfiguration<GroupsRight>
    {
        protected override string TableName { get; } = nameof(GroupsRight);
        
        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<GroupsRight> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder
                .Property(groupsRights => groupsRights.Allowed)
                .IsRequired();
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<GroupsRight> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasOne(groupsRights => groupsRights.Group)
                .WithMany(group => group.GroupsRights)
                .HasForeignKey(groupsRights => groupsRights.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasOne(groupsRights => groupsRights.Right)
                .WithMany(right => right.GroupsRights)
                .HasForeignKey(groupsRights => groupsRights.RightId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}