using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class PostCategoryConfiguration : AbstractEntityConfiguration<PostCategory>
    {
        protected override string TableName { get; } = nameof(PostCategory);

        /// <inheritdoc />
        protected override void SetKeys(EntityTypeBuilder<PostCategory> modelBuilder)
        {
            modelBuilder
                .HasKey(postCategory => postCategory.Id);
        }

        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<PostCategory> modelBuilder)
        {
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<PostCategory> modelBuilder)
        {
            modelBuilder
                .HasOne(postCategory => postCategory.Category)
                .WithMany(category => category.PostCategories)
                .HasForeignKey(postCategory => postCategory.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void SetIndexes(EntityTypeBuilder<PostCategory> modelBuilder)
        {
        }
    }
}