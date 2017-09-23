using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class PostRatingConfiguration : AbstractEntityConfiguration<PostRating>
    {
        protected override string TableName { get; } = nameof(PostRating);
        
        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<PostRating> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder
                .Property(postRating => postRating.Value)
                .IsRequired();
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<PostRating> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

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
    }
}