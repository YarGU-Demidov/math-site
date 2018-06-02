﻿using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
    public class PostUserAllowedSeeder : AbstractSeeder<PostUserAllowed>
    {
        /// <inheritdoc />
        public PostUserAllowedSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
        {
        }

        /// <inheritdoc />
        public override string SeedingObjectName { get; } = nameof(PostUserAllowed);


        /// <inheritdoc />
        protected override void SeedData()
        {
            var firstPostUsersAllowed = CreatePostUsersAllowed(
                GetPostByTitle(PostAliases.FirstPost),
                GetUserByLogin(UsersAliases.Mokeev1995)
            );

            var secondPostUsersAllowed = CreatePostUsersAllowed(
                GetPostByTitle(PostAliases.SecondPost),
                GetUserByLogin(UsersAliases.AndreyDevyatkin)
            );

            var postsUsersAlloweds = new[]
            {
                firstPostUsersAllowed,
                secondPostUsersAllowed
            };

            Context.PostUserAlloweds.AddRange(postsUsersAlloweds);
        }

        private static PostUserAllowed CreatePostUsersAllowed(Post post, User user)
        {
            return new PostUserAllowed
            {
                Post = post,
                User = user
            };
        }

        private User GetUserByLogin(string login)
        {
            return Context.Users.First(user => user.Login == login);
        }

        private Post GetPostByTitle(string title)
        {
            return Context.Posts.First(post => post.Title == title);
        }
    }
}