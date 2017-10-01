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
        protected override void SetRelationships(EntityTypeBuilder<PostCategory> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasOne(postCategory => postCategory.Category)
                .WithMany(category => category.PostCategories)
                .HasForeignKey(postCategory => postCategory.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}