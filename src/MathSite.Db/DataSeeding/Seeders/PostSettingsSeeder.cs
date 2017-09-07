using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostSettingsSeeder : AbstractSeeder<PostSetting>
	{
		/// <inheritdoc />
		public PostSettingsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(PostSetting);

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostSettings = CreatePostSettings(
				GetPostTypeByAlias(PostTypeAliases.News),
				GetPreviewImageByName(FileAliases.FirstFile),
				true,
				true,
				true
			);

			var secondPostSettings = CreatePostSettings(
				GetPostTypeByAlias(PostTypeAliases.StaticPage),
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

		private PostType GetPostTypeByAlias(string alias)
		{
			return Context.PostTypes.First(postType => postType.Alias == alias);
		}

		private File GetPreviewImageByName(string name)
		{
			return Context.Files.First(file => file.FileName == name);
		}

		private static PostSetting CreatePostSettings(PostType postType, File previewImage,
			bool isCommentsAllowed, bool canBeRated, bool isPostOnStartPage)
		{
			return new PostSetting
			{
				IsCommentsAllowed = isCommentsAllowed,
				CanBeRated = canBeRated,
				PostOnStartPage = isPostOnStartPage,
				PostType = postType,
				PreviewImage = previewImage
			};
		}
	}
}