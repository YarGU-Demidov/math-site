using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class GroupRightsConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GroupsRights>()
				.HasKey(groupsRights => groupsRights.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GroupsRights>()
				.Property(groupsRights => groupsRights.Allowed)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GroupsRights>()
				.HasOne(groupsRights => groupsRights.Group)
				.WithMany(group => group.GroupsRights)
				.HasForeignKey(groupsRights => groupsRights.GroupId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<GroupsRights>()
				.HasOne(groupsRights => groupsRights.Right)
				.WithMany(right => right.GroupsRights)
				.HasForeignKey(groupsRights => groupsRights.RightId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "GroupRights";
	}
}