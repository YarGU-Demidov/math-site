using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class GroupConfiguration : AbstractEntityWithAliasConfiguration<Group>
    {
        protected override string TableName { get; } = nameof(Group);
        
        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<Group> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder
                .Property(group => group.Name)
                .IsRequired();

            modelBuilder
                .Property(group => group.Description)
                .IsRequired(false);
            
            modelBuilder.Property(group => group.IsAdmin)
                .IsRequired();
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<Group> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasMany(group => group.Users)
                .WithOne(user => user.Group)
                .HasForeignKey(user => user.GroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(group => group.GroupsRights)
                .WithOne(groupRights => groupRights.Group)
                .HasForeignKey(groupRights => groupRights.GroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasOne(group => group.GroupType)
                .WithMany(groupType => groupType.Groups)
                .HasForeignKey(groupType => groupType.GroupTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasOne(group => group.ParentGroup)
                .WithMany(parentGroup => parentGroup.ChildGroups)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(group => group.ChildGroups)
                .WithOne(parentGroup => parentGroup.ParentGroup)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(group => group.PostGroupsAllowed)
                .WithOne(postGroupsAllowed => postGroupsAllowed.Group)
                .HasForeignKey(postGroupsAllowed => postGroupsAllowed.GroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}