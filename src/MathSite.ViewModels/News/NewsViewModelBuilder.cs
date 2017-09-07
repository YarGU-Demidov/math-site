using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.SharedModels.PostPreview;
using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.ViewModels.News
{
	public class NewsViewModelBuilder : SecondaryViewModelBuilder, INewsViewModelBuilder
	{
		public NewsViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade,
			IPostPreviewViewModelBuilder postPreviewViewModelBuilder)
			: base(siteSettingsFacade, postsFacade, postPreviewViewModelBuilder)
		{
		}

		protected override string PageTitle { get; set; }

		public async Task<NewsIndexViewModel> BuildIndexViewModelAsync(int page = 1)
		{
			await FillIndexPageNameAsync();

			var model = await BuildSecondaryViewModel<NewsIndexViewModel>();

			await BuildPosts(model, page);

			return model;
		}

		public async Task<NewsItemViewModel> BuildNewsItemViewModelAsync(string query, int page = 1)
		{
			var model = await BuildSecondaryViewModel<NewsItemViewModel>();

			var post = await BuildPostData(query, page);

			if (post == null)
				throw new PostNotFoundException();

			model.Content = post.Content;
			model.PageTitle.Title = post.Title;

			return model;
		}


		private async Task FillIndexPageNameAsync()
		{
			var title = await SiteSettingsFacade[SiteSettingsNames.DefaultNewsPageTitle];

			PageTitle = title ?? "Новости нашего факультета";
		}

		private async Task BuildPosts(NewsIndexViewModel model, int page)
		{
			var posts = await PostsFacade.GetNewsAsync(page);

			model.Posts = GetPosts(posts);
		}

		private IEnumerable<PostPreviewViewModel> GetPosts(IEnumerable<Post> posts)
		{
			return posts.Select(PostPreviewViewModelBuilder.Build);
		}

		private async Task<Post> BuildPostData(string query, int page = 1)
		{
			return await PostsFacade.GetNewsPostByUrlAsync(query);
		}
	}
}