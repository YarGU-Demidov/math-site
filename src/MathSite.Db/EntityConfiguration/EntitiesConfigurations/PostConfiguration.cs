using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Post>()
				.HasKey(post => post.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Post>()
				.Property(post => post.Title)
				.IsRequired();

			modelBuilder.Entity<Post>()
				.Property(post => post.Excerpt)
				.IsRequired();

			modelBuilder.Entity<Post>()
				.Property(post => post.Content)
				.IsRequired();

			modelBuilder.Entity<Post>()
				.Property(post => post.PublishDate)
				.IsRequired();

			modelBuilder.Entity<Post>()
				.Property(post => post.Published)
				.IsRequired();

			modelBuilder.Entity<Post>()
				.Property(post => post.Deleted)
				.IsRequired(false);
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Post>()
				.HasOne(post => post.PostType)
				.WithMany(postType => postType.Posts)
				.HasForeignKey(postType => postType.PostTypeId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Post>()
				.HasMany(post => post.PostCategories)
				.WithOne(postCategory => postCategory.Post)
				.HasForeignKey(postCategory => postCategory.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Post>()
				.HasOne(post => post.PostSeoSettings)
				.WithOne(postSeoSetting => postSeoSetting.Post)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Post>()
				.HasMany(post => post.PostAttachments)
				.WithOne(postAttachment => postAttachment.Post)
				.HasForeignKey(postAttachment => postAttachment.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Post>()
				.HasOne(post => post.PostSettings)
				.WithOne(postSetting => postSetting.Post)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Post>()
				.HasMany(post => post.Comments)
				.WithOne(comment => comment.Post)
				.HasForeignKey(comment => comment.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Post>()
				.HasMany(post => post.GroupsAllowed)
				.WithOne(groupAllowed => groupAllowed.Post)
				.HasForeignKey(groupAllowed => groupAllowed.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Post>()
				.HasMany(post => post.PostOwners)
				.WithOne(postOwner => postOwner.Post)
				.HasForeignKey(postOwner => postOwner.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Post>()
				.HasMany(post => post.PostRatings)
				.WithOne(postRating => postRating.Post)
				.HasForeignKey(postRating => postRating.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Post>()
				.HasMany(post => post.UsersAllowed)
				.WithOne(userAllowed => userAllowed.Post)
				.HasForeignKey(userAllowed => userAllowed.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "Post";
	}
}