using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class CommentConfiguration : AbstractEntityConfiguration<Comment>
	{
		protected override string TableName { get; } = nameof(Comment);

		/// <inheritdoc />
		protected override void SetKeys(EntityTypeBuilder<Comment> modelBuilder)
		{
			modelBuilder
				.HasKey(comment => comment.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<Comment> modelBuilder)
		{
			modelBuilder
				.Property(comment => comment.Text)
				.IsRequired();

			modelBuilder
				.Property(comment => comment.Date)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<Comment> modelBuilder)
		{
			modelBuilder
				.HasOne(comment => comment.User)
				.WithMany(user => user.Comments)
				.HasForeignKey(comment => comment.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(comment => comment.Post)
				.WithMany(post => post.Comments)
				.HasForeignKey(comment => comment.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		protected override void SetIndexes(EntityTypeBuilder<Comment> modelBuilder)
		{
		}
	}
}