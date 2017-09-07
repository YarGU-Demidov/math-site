using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.SharedModels.PostPreview;

namespace MathSite.ViewModels.Home
{
	public class HomeViewModelBuilder : CommonViewModelBuilder, IHomeViewModelBuilder
	{
		private readonly IPostsFacade _postsFacade;
		private readonly IPostPreviewViewModelBuilder _postPreviewViewModelBuilder;

		public HomeViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade, IPostPreviewViewModelBuilder postPreviewViewModelBuilder) 
			: base(siteSettingsFacade)
		{
			_postsFacade = postsFacade;
			_postPreviewViewModelBuilder = postPreviewViewModelBuilder;
		}

		protected override string PageTitle { get; set; }
		
		private async Task BuildPostsAsync(HomeIndexViewModel model)
		{
			var posts = await _postsFacade.GetLastSelectedForMainPagePostsAsync(6);
			model.Posts = GetPostsModels(posts);
		}

		private IEnumerable<PostPreviewViewModel> GetPostsModels(IEnumerable<Post> posts)
		{
			return posts.Select(_postPreviewViewModelBuilder.Build);
		}

		public async Task<HomeIndexViewModel> BuildIndexModel()
		{
			await FillPageNameAsync();
			var model = await BuildCommonViewModelAsync<HomeIndexViewModel>();
			await BuildPostsAsync(model);
			return model;
		}

		private async Task FillPageNameAsync()
		{
			var title = await SiteSettingsFacade[SiteSettingsNames.DefaultHomePageTitle];

			PageTitle = title ?? "Главная страница";
		}
	}
}