using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class CategoryConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasKey(category => category.Id);

			modelBuilder.Entity<Category>()
				.HasAlternateKey(category => category.Alias);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.Property(category => category.Name)
				.IsRequired();

			modelBuilder.Entity<Category>()
				.Property(category => category.Description)
				.IsRequired(false);

			modelBuilder.Entity<Category>()
				.Property(category => category.Alias)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasMany(category => category.PostCategories)
				.WithOne(postCategories => postCategories.Category)
				.HasForeignKey(postCategories => postCategories.CategoryId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "Category";
	}
}