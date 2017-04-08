using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class UsersConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasKey(u => u.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.Property(u => u.Login)
				.IsRequired();
			modelBuilder.Entity<User>()
				.Property(u => u.PasswordHash)
				.IsRequired();
		    modelBuilder.Entity<User>()
		        .Property(u => u.CreationDate)
		        .IsRequired();
        }

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasOne(user => user.Person)
				.WithOne(person => person.User)
				.HasForeignKey<Person>(person => person.UserId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<User>()
				.HasOne(user => user.Group)
				.WithMany(group => group.Users)
				.HasForeignKey(user => user.GroupId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<User>()
				.HasMany(user => user.UserRights)
				.WithOne(usersRights => usersRights.User)
				.HasForeignKey(usersRights => usersRights.UserId)
				.OnDelete(DeleteBehavior.Cascade);

		    modelBuilder.Entity<User>()
		        .HasMany(user => user.Settings)
		        .WithOne(settings => settings.User)
		        .HasForeignKey(settings => settings.UserId)
		        .OnDelete(DeleteBehavior.Cascade);

		    modelBuilder.Entity<User>()
		        .HasMany(user => user.PostsOwner)
		        .WithOne(postsOwner => postsOwner.User)
		        .HasForeignKey(postsOwner => postsOwner.UserId)
		        .OnDelete(DeleteBehavior.Cascade);

		    modelBuilder.Entity<User>()
		        .HasMany(user => user.AllowedPosts)
		        .WithOne(allowedPosts => allowedPosts.User)
		        .HasForeignKey(allowedPosts => allowedPosts.UserId)
		        .OnDelete(DeleteBehavior.Cascade);

		    modelBuilder.Entity<User>()
		        .HasMany(user => user.PostsRatings)
		        .WithOne(postsRatings => postsRatings.User)
		        .HasForeignKey(postsRatings => postsRatings.UserId)
		        .OnDelete(DeleteBehavior.Cascade);

		    modelBuilder.Entity<User>()
		        .HasMany(user => user.Comments)
		        .WithOne(comments => comments.User)
		        .HasForeignKey(comments => comments.UserId)
		        .OnDelete(DeleteBehavior.Cascade);
        }

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "User";
	}
}