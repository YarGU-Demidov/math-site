using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class CategoryConfiguration : AbstractEntityConfiguration<Category>
    {
        protected override string TableName { get; } = nameof(Category);

        /// <inheritdoc />
        protected override void SetKeys(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder
                .HasKey(category => category.Id);

            modelBuilder
                .HasAlternateKey(category => category.Alias);
        }

        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder
                .Property(category => category.Name)
                .IsRequired();

            modelBuilder
                .Property(category => category.Description)
                .IsRequired(false);

            modelBuilder
                .Property(category => category.Alias)
                .IsRequired();
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder
                .HasMany(category => category.PostCategories)
                .WithOne(postCategories => postCategories.Category)
                .HasForeignKey(postCategories => postCategories.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void SetIndexes(EntityTypeBuilder<Category> modelBuilder)
        {
        }
    }
}