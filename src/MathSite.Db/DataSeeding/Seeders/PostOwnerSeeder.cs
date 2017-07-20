using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostOwnerSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public PostOwnerSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "PostOwner";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.PostOwners.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostOwner = CreatePostOwner(
				GetPostByTitle(PostAliases.FirstPost),
				GetUserByLogin(UsersAliases.FirstUser)
			);

			var secondPostOwner = CreatePostOwner(
				GetPostByTitle(PostAliases.SecondPost),
				GetUserByLogin(UsersAliases.SecondUser)
			);


			var posts = new[]
			{
				firstPostOwner,
				secondPostOwner
			};

			Context.PostOwners.AddRange(posts);
		}

		private User GetUserByLogin(string login)
		{
			return Context.Users.First(user => user.Login == login);
		}

		private Post GetPostByTitle(string title)
		{
			return Context.Posts.First(post => post.Title == title);
		}

		private static PostOwner CreatePostOwner(Post post, User user)
		{
			return new PostOwner
			{
				Post = post,
				User = user
			};
		}
	}
}