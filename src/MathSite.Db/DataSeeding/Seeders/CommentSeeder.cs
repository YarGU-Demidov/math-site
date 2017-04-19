using System;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class CommentSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public CommentSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "Comment";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.Comments.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstComment = CreateComment(
				"My best comment",
				DateTime.Now,
				GetPostByTitle(PostAliases.FirstPost),
				GetUserByLogin(UsersAliases.FirstUser)
			);

			var secondComment = CreateComment(
				"Oh-la-la",
				DateTime.Now,
				GetPostByTitle(PostAliases.SecondPost),
				GetUserByLogin(UsersAliases.SecondUser)
			);

			var comments = new[]
			{
				firstComment,
				secondComment
			};

			Context.Comments.AddRange(comments);
		}

		private User GetUserByLogin(string login)
		{
			return Context.Users.First(user => user.Login == login);
		}

		private Post GetPostByTitle(string title)
		{
			return Context.Posts.First(post => post.Title == title);
		}

		private static Comment CreateComment(string text, DateTime commentDate, Post post, User user)
		{
			return new Comment
			{
				Text = text,
				Date = commentDate,
				Post = post,
				User = user
			};
		}
	}
}