using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostRatingConfiguration : AbstractEntityConfiguration<PostRating>
	{
		/// <inheritdoc />
		protected override void SetKeys(EntityTypeBuilder<PostRating> modelBuilder)
		{
			modelBuilder
				.HasKey(postRating => postRating.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<PostRating> modelBuilder)
		{
			modelBuilder
				.Property(postRating => postRating.Value)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<PostRating> modelBuilder)
		{
			modelBuilder
				.HasOne(postRating => postRating.User)
				.WithMany(user => user.PostsRatings)
				.HasForeignKey(postRating => postRating.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(postRating => postRating.Post)
				.WithMany(post => post.PostRatings)
				.HasForeignKey(postRating => postRating.PostId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		protected override void SetIndexes(EntityTypeBuilder<PostRating> modelBuilder)
		{
		}
	}
}