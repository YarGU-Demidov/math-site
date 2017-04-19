using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostAttachmentConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostAttachment>()
				.HasKey(postAttachment => postAttachment.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostAttachment>()
				.Property(postAttachment => postAttachment.Allowed)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostAttachment>()
				.HasOne(postAttachment => postAttachment.Post)
				.WithMany(posts => posts.PostAttachments)
				.HasForeignKey(postAttachment => postAttachment.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<PostAttachment>()
				.HasOne(postAttachment => postAttachment.File)
				.WithMany(file => file.PostAttachments)
				.HasForeignKey(postAttachment => postAttachment.FileId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "PostAttachment";
	}
}