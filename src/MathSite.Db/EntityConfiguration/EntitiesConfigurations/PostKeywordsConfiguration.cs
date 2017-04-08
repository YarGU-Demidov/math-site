using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostKeywordsConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostKeywords>()
				.HasKey(postKeywords => postKeywords.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostKeywords>()
				.HasOne(postKeywords => postKeywords.Keyword)
				.WithMany(post => post.Posts)
				.HasForeignKey(postSeoSettings => postSeoSettings.KeywordId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<PostKeywords>()
				.HasOne(postKeywords => postKeywords.PostSeoSettings)
				.WithMany(postSeoSettings => postSeoSettings.PostKeywords)
				.HasForeignKey(postKeywords => postKeywords.PostSeoSettingsId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "PostKeywords";
	}
}
