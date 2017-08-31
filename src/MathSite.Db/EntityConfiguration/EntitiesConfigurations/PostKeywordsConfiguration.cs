using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostKeywordsConfiguration : AbstractEntityConfiguration<PostKeywords>
	{
		/// <inheritdoc />
		protected override void SetKeys(EntityTypeBuilder<PostKeywords> modelBuilder)
		{
			modelBuilder
				.HasKey(postKeywords => postKeywords.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<PostKeywords> modelBuilder)
		{
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<PostKeywords> modelBuilder)
		{
			modelBuilder
				.HasOne(postKeywords => postKeywords.Keyword)
				.WithMany(post => post.Posts)
				.HasForeignKey(postSeoSettings => postSeoSettings.KeywordId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(postKeywords => postKeywords.PostSeoSettings)
				.WithMany(postSeoSettings => postSeoSettings.PostKeywords)
				.HasForeignKey(postKeywords => postKeywords.PostSeoSettingsId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}