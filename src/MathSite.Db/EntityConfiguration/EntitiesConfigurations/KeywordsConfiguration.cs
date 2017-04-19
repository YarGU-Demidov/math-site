using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class KeywordsConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Keywords>()
				.HasKey(keyword => keyword.Id);

			modelBuilder.Entity<Keywords>()
				.HasAlternateKey(keyword => keyword.Alias);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Keywords>()
				.Property(keyword => keyword.Name)
				.IsRequired();

			modelBuilder.Entity<Keywords>()
				.Property(keyword => keyword.Alias)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Keywords>()
				.HasMany(keyword => keyword.Posts)
				.WithOne(posts => posts.Keyword)
				.HasForeignKey(keyword => keyword.KeywordId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "Keywords";
	}
}