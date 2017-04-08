using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class PostRatingConfiguration : AbstractEntityConfiguration
    {
        /// <inheritdoc />
        protected override void SetPrimaryKey(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostRating>()
                .HasKey(postRating => postRating.Id);
        }

        /// <inheritdoc />
        protected override void SetFields(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostRating>()
                .Property(postRating => postRating.Value)
                .IsRequired();
        }

        /// <inheritdoc />
        protected override void SetRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostRating>()
                .HasOne(postRating => postRating.User)
                .WithMany(user => user.PostsRatings)
                .HasForeignKey(postRating => postRating.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostRating>()
                .HasOne(postRating => postRating.Post)
                .WithMany(post => post.PostRatings)
                .HasForeignKey(postRating => postRating.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        /// <inheritdoc />
        public override string ConfigurationName { get; } = "PostRating";
    }
}
