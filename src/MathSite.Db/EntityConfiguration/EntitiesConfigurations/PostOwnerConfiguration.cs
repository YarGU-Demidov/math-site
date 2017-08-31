using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostOwnerConfiguration : AbstractEntityConfiguration<PostOwner>
	{
		/// <inheritdoc />
		protected override void SetKeys(EntityTypeBuilder<PostOwner> modelBuilder)
		{
			modelBuilder
				.HasKey(postOwner => postOwner.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<PostOwner> modelBuilder)
		{
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<PostOwner> modelBuilder)
		{
			modelBuilder
				.HasOne(postOwner => postOwner.User)
				.WithMany(user => user.PostsOwner)
				.HasForeignKey(postOwner => postOwner.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(postOwner => postOwner.Post)
				.WithMany(post => post.PostOwners)
				.HasForeignKey(postOwner => postOwner.PostId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}