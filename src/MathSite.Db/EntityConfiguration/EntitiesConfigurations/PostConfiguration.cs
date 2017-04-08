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
                .HasForeignKey(post => post.PostTypeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.PostCategories)
                .WithOne(postCategories => postCategories.Post)
                .HasForeignKey(post => post.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.PostSeoSettings)
                .WithOne(postSeoSettings => postSeoSettings.Post)
                .HasForeignKey(post => post.PostId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.PostAttachments)
                .WithOne(postAttachments => postAttachments.Post)
                .HasForeignKey(post => post.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.PostSettings)
                .WithOne(postSettings => postSettings.Post)
                .HasForeignKey(post => post.PostId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.Comments)
                .WithOne(comments => comments.Post)
                .HasForeignKey(post => post.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.GroupsAllowed)
                .WithOne(groupsAllowed => groupsAllowed.Post)
                .HasForeignKey(post => post.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.PostOwners)
                .WithOne(postOwners => postOwners.Post)
                .HasForeignKey(post => post.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.PostRatings)
                .WithOne(postRatings => postRatings.Post)
                .HasForeignKey(post => post.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.UsersAllowed)
                .WithOne(usersAllowed => usersAllowed.Post)
                .HasForeignKey(post => post.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        /// <inheritdoc />
        public override string ConfigurationName { get; } = "Keyword";
    }
}
