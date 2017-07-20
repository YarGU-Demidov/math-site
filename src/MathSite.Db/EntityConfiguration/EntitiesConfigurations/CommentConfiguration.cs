using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class CommentConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Comment>()
				.HasKey(comment => comment.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Comment>()
				.Property(comment => comment.Text)
				.IsRequired();

			modelBuilder.Entity<Comment>()
				.Property(comment => comment.Date)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Comment>()
				.HasOne(comment => comment.User)
				.WithMany(user => user.Comments)
				.HasForeignKey(comment => comment.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Comment>()
				.HasOne(comment => comment.Post)
				.WithMany(post => post.Comments)
				.HasForeignKey(comment => comment.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "Comment";
	}
}