using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class PostTypeConfiguration : AbstractEntityConfiguration<PostType>
	{
		protected override string TableName { get; } = nameof(PostType);

		/// <inheritdoc />
		protected override void SetKeys(EntityTypeBuilder<PostType> modelBuilder)
		{
			modelBuilder
				.HasKey(postType => postType.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<PostType> modelBuilder)
		{
			modelBuilder
				.Property(postType => postType.TypeName)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<PostType> modelBuilder)
		{
			modelBuilder
				.HasMany(postType => postType.Posts)
				.WithOne(posts => posts.PostType)
				.HasForeignKey(postType => postType.PostTypeId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(postType => postType.DefaultPostsSettings)
				.WithOne(defaultPostsSettings => defaultPostsSettings.PostType)
				.HasForeignKey<PostSettings>(defaultPostsSettings => defaultPostsSettings.PostTypeId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		protected override void SetIndexes(EntityTypeBuilder<PostType> modelBuilder)
		{
		}
	}
}