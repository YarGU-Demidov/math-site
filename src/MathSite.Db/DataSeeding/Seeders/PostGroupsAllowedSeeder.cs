using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostGroupsAllowedSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public PostGroupsAllowedSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "PostGroupsAllowed";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.PostGroupsAlloweds.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostGroupsAllowed = CreatePostGroupsAllowed(
				GetPostByTitle(PostAliases.FirstPost),
				GetGroupByGroupTypeAlias(GroupTypeAliases.Employee)
			);

			var secondPostGroupsAllowed = CreatePostGroupsAllowed(
				GetPostByTitle(PostAliases.SecondPost),
				GetGroupByGroupTypeAlias(GroupTypeAliases.Student)
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
			return Context.Groups.First(group => group.Name == alias);
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