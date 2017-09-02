using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.SharedModels;

namespace MathSite.ViewModels.Home
{
	public interface IHomeViewModelBuilder
	{
		Task<HomeIndexViewModel> BuildIndexModel();
	}

	public class HomeViewModelBuilder : CommonViewModelBuilder, IHomeViewModelBuilder
	{
		public HomeViewModelBuilder(ISiteSettingsFacade siteSettingsFacade) 
			: base(siteSettingsFacade)
		{
		}

		protected override string PageTitle { get; } = "Главная страница";

		private async Task BuildPostsAsync(HomeIndexViewModel model)
		{
			var posts = new List<PostPreviewViewModel>
			{
				new PostPreviewViewModel("Test1", "/news/test1-url", "Test content for 1st post", DateTime.Now.ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test2", "/news/test2-url", "Test content for 2nd post", DateTime.Now.AddMonths(-2).AddDays(-1).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test3", "/news/test3-url", "Test content for 3d post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test4", "/news/test4-url", "Test content for 4th post", DateTime.Now.AddDays(-3).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test5", "/news/test5-url", "Test content for 5th post", DateTime.Now.AddMonths(-7).AddDays(-15).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test6", "/news/test6-url", "Test content for 6th post", DateTime.Now.AddMonths(-5).AddDays(-10).ToString("dd MMM yyyy", new CultureInfo("ru")))
			};

			model.Posts = posts;
		}

		public async Task<HomeIndexViewModel> BuildIndexModel()
		{
			var model = await BuildCommonViewModelAsync<HomeIndexViewModel>();
			await BuildPostsAsync(model);

			return model;
		}
	}
}