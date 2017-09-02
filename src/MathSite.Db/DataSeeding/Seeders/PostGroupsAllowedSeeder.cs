using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostGroupsAllowedSeeder : AbstractSeeder<PostGroupsAllowed>
	{
		/// <inheritdoc />
		public PostGroupsAllowedSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(PostGroupsAllowed);

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostGroupsAllowed = CreatePostGroupsAllowed(
				GetPostByTitle(PostAliases.FirstPost),
				GetGroupByGroupTypeAlias(GroupTypeAliases.Employee)
			);

			var secondPostGroupsAllowed = CreatePostGroupsAllowed(
				GetPostByTitle(PostAliases.SecondPost),
				GetGroupByGroupTypeAlias(GroupTypeAliases.User)
			);

			var postGroupsAlloweds = new[]
			{
				firstPostGroupsAllowed,
				secondPostGroupsAllowed
			};

			Context.PostGroupsAlloweds.AddRange(postGroupsAlloweds);
		}

		private Group GetGroupByGroupTypeAlias(string alias)
		{
			return Context.Groups
				.Include(group => group.GroupType)
				.First(group => group.GroupType.Alias == alias);
		}

		private Post GetPostByTitle(string title)
		{
			return Context.Posts.First(post => post.Title == title);
		}

		private static PostGroupsAllowed CreatePostGroupsAllowed(Post post, Group group)
		{
			return new PostGroupsAllowed
			{
				Post = post,
				Group = group
			};
		}
	}
}