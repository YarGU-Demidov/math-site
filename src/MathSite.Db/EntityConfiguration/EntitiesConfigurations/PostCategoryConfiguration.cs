using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class PostCategoryConfiguration : AbstractEntityConfiguration
    {
        /// <inheritdoc />
        protected override void SetPrimaryKey(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostCategory>()
                .HasKey(postCategory => postCategory.Id);
        }

        /// <inheritdoc />
        protected override void SetFields(ModelBuilder modelBuilder) { }

        /// <inheritdoc />
        protected override void SetRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostCategory>()
                .HasOne(postCategory => postCategory.Category)
                .WithMany(category => category.PostCategories)
                .HasForeignKey(postCategory => postCategory.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        /// <inheritdoc />
        public override string ConfigurationName { get; } = "PostCategory";
    }
}
