using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostGroupsAllowedConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostGroupsAllowed>()
				.HasKey(postGroupsAllowed => postGroupsAllowed.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostGroupsAllowed>()
				.Property(postGroupsAllowed => postGroupsAllowed.Allowed)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostGroupsAllowed>()
				.HasOne(postGroupsAllowed => postGroupsAllowed.Post)
				.WithMany(post => post.GroupsAllowed)
				.HasForeignKey(postGroupsAllowed => postGroupsAllowed.PostId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<PostGroupsAllowed>()
				.HasOne(postGroupsAllowed => postGroupsAllowed.Group)
				.WithMany(post => post.PostGroupsAllowed)
				.HasForeignKey(postGroupsAllowed => postGroupsAllowed.GroupId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "PostGroupsAllowed";
	}
}