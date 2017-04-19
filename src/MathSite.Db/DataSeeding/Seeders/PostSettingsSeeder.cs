using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostSettingsSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public PostSettingsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "PostSettings";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.PostSettings.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostSettings = CreatePostSettings(
				GetPostByTitle(PostAliases.FirstPost),
				GetPostTypeByName(PostTypeAliases.News),
				GetPreviewImageByName(FileAliases.FirstFile),
				true,
				true,
				true
			);

			var secondPostSettings = CreatePostSettings(
				GetPostByTitle(PostAliases.SecondPost),
				GetPostTypeByName(PostTypeAliases.StaticPage),
				GetPreviewImageByName(FileAliases.SecondFile),
				false,
				false,
				false
			);

			var postsSettings = new[]
			{
				firstPostSettings,
				secondPostSettings
			};

			Context.PostSettings.AddRange(postsSettings);
		}

		private Post GetPostByTitle(string title)
		{
			return Context.Posts.FirstOrDefault(post => post.Title == title);
		}

		private PostType GetPostTypeByName(string name)
		{
			return Context.PostTypes.FirstOrDefault(postType => postType.TypeName == name);
		}

		private File GetPreviewImageByName(string name)
		{
			return Context.Files.First(file => file.FileName == name);
		}

		private static PostSettings CreatePostSettings(Post post, PostType postType, File previewImage,
			bool isCommentsAllowed,
			bool canBeRated, bool isPostOnStartPage)
		{
			return new PostSettings
			{
				IsCommentsAllowed = isCommentsAllowed,
				CanBeRated = canBeRated,
				PostOnStartPage = isPostOnStartPage,
				Post = post,
				PostType = postType,
				PreviewImage = previewImage
			};
		}
	}
}