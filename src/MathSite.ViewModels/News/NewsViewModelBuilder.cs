﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.SharedModels;
using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.ViewModels.News
{
	public interface INewsViewModelBuilder
	{
		Task<NewsIndexViewModel> BuildIndexViewModelAsync();
	}

	public class NewsViewModelBuilder: SecondaryViewModelBuilder, INewsViewModelBuilder
	{
		public NewsViewModelBuilder(ISiteSettingsFacade siteSettingsFacade) 
			: base(siteSettingsFacade)
		{
		}

		protected override string PageTitle { get; } = "Новости нашего факультета";

		public async Task<NewsIndexViewModel> BuildIndexViewModelAsync()
		{
			var model = await BuildSecondaryViewModel<NewsIndexViewModel>();

			await BuildPosts(model);

			return model;
		}

		private async Task BuildPosts(NewsIndexViewModel model)
		{
			model.Posts = new List<PostPreviewViewModel>
			{
				new PostPreviewViewModel("Test1", "/news/test1-url", "Test content for 1st post", DateTime.Now.ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test2", "/news/test2-url", "Test content for 2nd post", DateTime.Now.AddMonths(-2).AddDays(-1).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test3", "/news/test3-url", "Test content for 3d post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test4", "/news/test4-url", "Test content for 4th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test5", "/news/test5-url", "Test content for 5th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test6", "/news/test6-url", "Test content for 6th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test7", "/news/test7-url", "Test content for 7th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test8", "/news/test8-url", "Test content for 8th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test9", "/news/test9-url", "Test content for 9th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test10", "/news/test10-url", "Test content for 10th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test11", "/news/test11-url", "Test content for 11th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test12", "/news/test12-url", "Test content for 12th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test13", "/news/test13-url", "Test content for 13th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test14", "/news/test14-url", "Test content for 14th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test15", "/news/test15-url", "Test content for 15th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test16", "/news/test16-url", "Test content for 16th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru")))
			};
		}
	}
}