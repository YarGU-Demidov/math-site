using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostUsersAllowedConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostUserAllowed>()
				.HasKey(postUserAllowed => postUserAllowed.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostUserAllowed>()
				.HasOne(postUserAllowed => postUserAllowed.User)
				.WithMany(user => user.AllowedPosts)
				.HasForeignKey(postUserAllowed => postUserAllowed.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<PostUserAllowed>()
				.HasOne(postUserAllowed => postUserAllowed.Post)
				.WithMany(post => post.UsersAllowed)
				.HasForeignKey(postUserAllowed => postUserAllowed.PostId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "PostUserAllowed";
	}
}
