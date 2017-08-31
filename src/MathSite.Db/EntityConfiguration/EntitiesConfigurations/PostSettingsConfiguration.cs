using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostSettingsConfiguration : AbstractEntityConfiguration<PostSettings>
	{
		/// <inheritdoc />
		protected override void SetKeys(EntityTypeBuilder<PostSettings> modelBuilder)
		{
			modelBuilder
				.HasKey(postSettings => postSettings.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<PostSettings> modelBuilder)
		{
			modelBuilder
				.Property(postSettings => postSettings.IsCommentsAllowed)
				.IsRequired(false);

			modelBuilder
				.Property(postSettings => postSettings.CanBeRated)
				.IsRequired(false);

			modelBuilder
				.Property(postSettings => postSettings.PostOnStartPage)
				.IsRequired(false);
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<PostSettings> modelBuilder)
		{
			modelBuilder
				.HasOne(postSettings => postSettings.PostType)
				.WithOne(postType => postType.DefaultPostsSettings)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(postSettings => postSettings.Post)
				.WithOne(post => post.PostSettings)
				.HasForeignKey<Post>(post => post.PostSettingsId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(postSettings => postSettings.PreviewImage)
				.WithMany(previewImage => previewImage.PostSettings)
				.HasForeignKey(postSettings => postSettings.PreviewImageId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}