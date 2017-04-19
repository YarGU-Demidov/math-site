using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class GroupConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Group>()
				.HasKey(group => group.Id);

			modelBuilder.Entity<Group>()
				.HasAlternateKey(group => group.Alias);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Group>()
				.Property(group => group.Name)
				.IsRequired();

			modelBuilder.Entity<Group>()
				.Property(group => group.Description)
				.IsRequired(false);

			modelBuilder.Entity<Group>()
				.Property(group => group.Alias)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Group>()
				.HasMany(group => group.Users)
				.WithOne(user => user.Group)
				.HasForeignKey(user => user.GroupId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Group>()
				.HasMany(group => group.GroupsRights)
				.WithOne(groupRights => groupRights.Group)
				.HasForeignKey(groupRights => groupRights.GroupId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Group>()
				.HasOne(group => group.GroupType)
				.WithMany(groupType => groupType.Groups)
				.HasForeignKey(groupType => groupType.GroupTypeId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Group>()
				.HasOne(group => group.ParentGroup)
				.WithOne(parentGroup => parentGroup.ParentGroup)
				.HasForeignKey<Group>(parentGroup => parentGroup.Id)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Group>()
				.HasMany(group => group.PostGroupsAllowed)
				.WithOne(postGroupsAllowed => postGroupsAllowed.Group)
				.HasForeignKey(postGroupsAllowed => postGroupsAllowed.GroupId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "Group";
	}
}