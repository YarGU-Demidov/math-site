using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class PostTypeConfiguration : AbstractEntityConfiguration
    {
        /// <inheritdoc />
        protected override void SetPrimaryKey(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostType>()
                .HasKey(postType => postType.Id);
        }

        /// <inheritdoc />
        protected override void SetFields(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostType>()
                .Property(postType => postType.TypeName)
                .IsRequired();
        }

        /// <inheritdoc />
        protected override void SetRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostType>()
                .HasMany(postType => postType.Posts)
                .WithOne(posts => posts.PostType)
                .HasForeignKey(postType => postType.PostTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostType>()
                .HasMany(postType => postType.DefaultPostsSettings)
                .WithOne(defaultPostsSettings => defaultPostsSettings.PostType)
                .HasForeignKey(postType => postType.PostTypeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }

        /// <inheritdoc />
        public override string ConfigurationName { get; } = "PostType";
    }
}
