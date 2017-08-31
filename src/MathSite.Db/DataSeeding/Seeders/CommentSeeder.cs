using System;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class CommentSeeder : AbstractSeeder<Comment>
	{
		/// <inheritdoc />
		public CommentSeeder(ILogger logger, IMathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(Comment);

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
				"Oh-la-la (edited comment)",
				DateTime.Now,
				GetPostByTitle(PostAliases.SecondPost),
				GetUserByLogin(UsersAliases.SecondUser),
				true
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

		private static Comment CreateComment(string text, DateTime commentDate, Post post, User user, bool edited = false)
		{
			return new Comment
			{
				Text = text,
				Date = commentDate,
				Post = post,
				User = user,
				Edited = edited
			};
		}
	}
}