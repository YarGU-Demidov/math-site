using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostUserAllowedConfiguration : AbstractEntityConfiguration<PostUserAllowed>
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(EntityTypeBuilder<PostUserAllowed> modelBuilder)
		{
			modelBuilder
				.HasKey(postUserAllowed => postUserAllowed.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<PostUserAllowed> modelBuilder)
		{
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<PostUserAllowed> modelBuilder)
		{
			modelBuilder
				.HasOne(postUserAllowed => postUserAllowed.User)
				.WithMany(user => user.AllowedPosts)
				.HasForeignKey(postUserAllowed => postUserAllowed.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(postUserAllowed => postUserAllowed.Post)
				.WithMany(post => post.UsersAllowed)
				.HasForeignKey(postUserAllowed => postUserAllowed.PostId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}