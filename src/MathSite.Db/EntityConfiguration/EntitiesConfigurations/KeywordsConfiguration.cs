using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class KeywordsConfiguration : AbstractEntityConfiguration<Keyword>
	{
		protected override string TableName { get; } = nameof(Keyword);

		/// <inheritdoc />
		protected override void SetKeys(EntityTypeBuilder<Keyword> modelBuilder)
		{
			modelBuilder
				.HasKey(keyword => keyword.Id);

			modelBuilder
				.HasAlternateKey(keyword => keyword.Alias);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<Keyword> modelBuilder)
		{
			modelBuilder
				.Property(keyword => keyword.Name)
				.IsRequired();

			modelBuilder
				.Property(keyword => keyword.Alias)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<Keyword> modelBuilder)
		{
			modelBuilder
				.HasMany(keyword => keyword.Posts)
				.WithOne(posts => posts.Keyword)
				.HasForeignKey(keyword => keyword.KeywordId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		protected override void SetIndexes(EntityTypeBuilder<Keyword> modelBuilder)
		{
		}
	}
}