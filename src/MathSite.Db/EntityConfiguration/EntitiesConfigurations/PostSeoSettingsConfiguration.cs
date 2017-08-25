using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostSeoSettingsConfiguration : AbstractEntityConfiguration<PostSeoSettings>
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(EntityTypeBuilder<PostSeoSettings> modelBuilder)
		{
			modelBuilder
				.HasKey(postSeoSettings => postSeoSettings.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<PostSeoSettings> modelBuilder)
		{
			modelBuilder
				.Property(postSeoSettings => postSeoSettings.Url)
				.IsRequired(false);

			modelBuilder
				.Property(postSeoSettings => postSeoSettings.Title)
				.IsRequired(false);

			modelBuilder
				.Property(postSeoSettings => postSeoSettings.Description)
				.IsRequired(false);
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<PostSeoSettings> modelBuilder)
		{
			modelBuilder
				.HasOne(postSeoSettings => postSeoSettings.Post)
				.WithOne(post => post.PostSeoSettings)
				.HasForeignKey<Post>(post => post.PostSeoSettingsId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasMany(postSeoSettings => postSeoSettings.PostKeywords)
				.WithOne(postKeywords => postKeywords.PostSeoSettings)
				.HasForeignKey(postSeoSettings => postSeoSettings.PostSeoSettingsId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}