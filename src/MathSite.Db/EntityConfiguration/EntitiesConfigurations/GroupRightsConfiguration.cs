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
		protected override void SetKeys(EntityTypeBuilder<GroupsRight> modelBuilder)
		{
			modelBuilder
				.HasKey(groupsRights => groupsRights.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<GroupsRight> modelBuilder)
		{
			modelBuilder
				.Property(groupsRights => groupsRights.Allowed)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<GroupsRight> modelBuilder)
		{
			modelBuilder
				.HasOne(groupsRights => groupsRights.Group)
				.WithMany(group => group.GroupsRights)
				.HasForeignKey(groupsRights => groupsRights.GroupId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(groupsRights => groupsRights.Right)
				.WithMany(right => right.GroupsRights)
				.HasForeignKey(groupsRights => groupsRights.RightAlias)
				.OnDelete(DeleteBehavior.Cascade);
		}

		protected override void SetIndexes(EntityTypeBuilder<GroupsRight> modelBuilder)
		{
		}
	}
}