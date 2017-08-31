using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostAttachmentConfiguration : AbstractEntityConfiguration<PostAttachment>
	{
		/// <inheritdoc />
		protected override void SetKeys(EntityTypeBuilder<PostAttachment> modelBuilder)
		{
			modelBuilder
				.HasKey(postAttachment => postAttachment.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<PostAttachment> modelBuilder)
		{
			modelBuilder
				.Property(postAttachment => postAttachment.Allowed)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<PostAttachment> modelBuilder)
		{
			modelBuilder
				.HasOne(postAttachment => postAttachment.Post)
				.WithMany(posts => posts.PostAttachments)
				.HasForeignKey(postAttachment => postAttachment.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(postAttachment => postAttachment.File)
				.WithMany(file => file.PostAttachments)
				.HasForeignKey(postAttachment => postAttachment.FileId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		protected override void SetIndexes(EntityTypeBuilder<PostAttachment> modelBuilder)
		{
		}
	}
}