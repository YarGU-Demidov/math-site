using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class CategoryConfiguration : AbstractEntityWithAliasConfiguration<Category>
    {
        protected override string TableName { get; } = nameof(Category);
        
        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<Category> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder
                .Property(category => category.Name)
                .IsRequired();

            modelBuilder
                .Property(category => category.Description)
                .IsRequired(false);
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<Category> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasMany(category => category.PostCategories)
                .WithOne(postCategories => postCategories.Category)
                .HasForeignKey(postCategories => postCategories.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}