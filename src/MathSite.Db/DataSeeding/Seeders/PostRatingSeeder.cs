using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostRatingSeeder : AbstractSeeder<PostRating>
	{
		/// <inheritdoc />
		public PostRatingSeeder(ILogger logger, IMathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(PostRating);

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostRating = CreatePostRating(
				GetPostByTitle(PostAliases.FirstPost),
				GetUserByLogin(UsersAliases.FirstUser),
				true
			);

			var secondPostRating = CreatePostRating(
				GetPostByTitle(PostAliases.SecondPost),
				GetUserByLogin(UsersAliases.SecondUser),
				false
			);


			var postsRatings = new[]
			{
				firstPostRating,
				secondPostRating
			};

			Context.PostRatings.AddRange(postsRatings);
		}

		private User GetUserByLogin(string login)
		{
			return Context.Users.First(user => user.Login == login);
		}

		private Post GetPostByTitle(string title)
		{
			return Context.Posts.First(post => post.Title == title);
		}

		private static PostRating CreatePostRating(Post post, User user, bool isLike)
		{
			return new PostRating
			{
				Post = post,
				User = user,
				Value = isLike
			};
		}
	}
}