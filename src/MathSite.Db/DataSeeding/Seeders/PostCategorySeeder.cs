using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostCategorySeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public PostCategorySeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "PostCategory";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.PostCategories.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostCategory = CreatePostCategoryAllowed(
				GetCategoryByName(CategoryAliases.Education),
				GetPostByTitle(PostAliases.FirstPost)
			);

			var secondPostCategory = CreatePostCategoryAllowed(
				GetCategoryByName(CategoryAliases.Career),
				GetPostByTitle(PostAliases.SecondPost)
			);

			var postsCategories = new[]
			{
				firstPostCategory,
				secondPostCategory
			};

			Context.PostCategories.AddRange(postsCategories);
		}

		private Category GetCategoryByName(string name)
		{
			return Context.Categories.First(category => category.Name == name);
		}

		private Post GetPostByTitle(string title)
		{
			return Context.Posts.First(post => post.Title == title);
		}

		private static PostCategory CreatePostCategoryAllowed(Category category, Post post)
		{
			return new PostCategory
			{
				Category = category,
				Post = post
			};
		}
	}
}