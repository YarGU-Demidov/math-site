using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostGroupsAllowedConfiguration : AbstractEntityConfiguration<PostGroupsAllowed>
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(EntityTypeBuilder<PostGroupsAllowed> modelBuilder)
		{
			modelBuilder
				.HasKey(postGroupsAllowed => postGroupsAllowed.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<PostGroupsAllowed> modelBuilder)
		{
			modelBuilder
				.Property(postGroupsAllowed => postGroupsAllowed.Allowed)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<PostGroupsAllowed> modelBuilder)
		{
			modelBuilder
				.HasOne(postGroupsAllowed => postGroupsAllowed.Post)
				.WithMany(post => post.GroupsAllowed)
				.HasForeignKey(postGroupsAllowed => postGroupsAllowed.PostId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(postGroupsAllowed => postGroupsAllowed.Group)
				.WithMany(post => post.PostGroupsAllowed)
				.HasForeignKey(postGroupsAllowed => postGroupsAllowed.GroupId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}