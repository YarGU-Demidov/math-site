using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostOwnerConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostOwner>()
				.HasKey(postOwner => postOwner.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostOwner>()
				.HasOne(postOwner => postOwner.User)
				.WithMany(user => user.PostsOwner)
				.HasForeignKey(postOwner => postOwner.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<PostOwner>()
				.HasOne(postOwner => postOwner.Post)
				.WithMany(post => post.PostOwners)
				.HasForeignKey(postOwner => postOwner.PostId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "PostOwner";
	}
}