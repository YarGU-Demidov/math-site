using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class PostOwnerConfiguration : AbstractEntityConfiguration<PostOwner>
    {
        protected override string TableName { get; } = nameof(PostOwner);
        
        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<PostOwner> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

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