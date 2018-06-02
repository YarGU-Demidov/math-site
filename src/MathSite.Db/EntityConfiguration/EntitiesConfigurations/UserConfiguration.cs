﻿using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class UserConfiguration : AbstractEntityConfiguration<User>
    {
        protected override string TableName { get; } = nameof(User);
        
        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<User> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder
                .Property(u => u.Login)
                .IsRequired();
            modelBuilder
                .Property(u => u.PasswordHash)
                .IsRequired();
            modelBuilder.Property(user => user.PersonId)
                .IsRequired();
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<User> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasOne(user => user.Person)
                .WithOne(person => person.User)
                .HasForeignKey<User>(person => person.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .HasOne(user => user.Group)
                .WithMany(group => group.Users)
                .HasForeignKey(user => user.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(user => user.UserRights)
                .WithOne(usersRights => usersRights.User)
                .HasForeignKey(usersRights => usersRights.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(user => user.Settings)
                .WithOne(settings => settings.User)
                .HasForeignKey(settings => settings.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(user => user.PostsOwner)
                .WithOne(postsOwner => postsOwner.User)
                .HasForeignKey(postsOwner => postsOwner.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(user => user.AllowedPosts)
                .WithOne(allowedPosts => allowedPosts.User)
                .HasForeignKey(allowedPosts => allowedPosts.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(user => user.PostsRatings)
                .WithOne(postsRatings => postsRatings.User)
                .HasForeignKey(postsRatings => postsRatings.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(user => user.Comments)
                .WithOne(comments => comments.User)
                .HasForeignKey(comments => comments.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(user => user.Posts)
                .WithOne(post => post.Author)
                .HasForeignKey(post => post.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}