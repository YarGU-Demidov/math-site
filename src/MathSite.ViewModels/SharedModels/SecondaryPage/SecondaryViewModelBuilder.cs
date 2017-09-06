using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;

namespace MathSite.ViewModels.SharedModels.SecondaryPage
{
	public abstract class SecondaryViewModelBuilder : CommonViewModelBuilder
	{
		protected IPostsFacade PostsFacade { get; }

		protected SecondaryViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade) 
			: base(siteSettingsFacade)
		{
			PostsFacade = postsFacade;
		}

		protected async Task<T> BuildSecondaryViewModel<T>()
			where T: SecondaryViewModel, new()
		{
			var model = await BuildCommonViewModelAsync<T>();
			await BuildSidebarMenuAsync(model);

			await BuildSidebarMenuAsync(model);
			await BuildFeaturedMenuAsync(model);

			return model;
		}

		private async Task BuildSidebarMenuAsync(SecondaryViewModel model)
		{
			model.SidebarMenuItems = new List<MenuItemViewModel>
			{
				new MenuItemViewModel(
					"Кафедры",
					"/departments",
					new List<MenuItemViewModel>
					{
						new MenuItemViewModel("Кафедра общей математики", "/departments/general-math"),
						new MenuItemViewModel("Кафедра математического анализа", "/departments/calculus"),
						new MenuItemViewModel("Кафедра компьютерной безопасности и математических методов обработки информации", "/departments/computer-security"),
						new MenuItemViewModel("Кафедра алгебры и математической логики", "/departments/algebra"),
						new MenuItemViewModel("Кафедра математического моделирования", "/departments/mathmod"),
						new MenuItemViewModel("Кафедра дифференциальных уравнений", "/departments/differential-equations")
					}
				)
			};
		}
		
		private async Task BuildFeaturedMenuAsync(SecondaryViewModel model)
		{
			var posts = await PostsFacade.GetLastSelectedForMainPagePostsAsync(3);

			model.Featured = GetPostsModels(posts);
		}


		private IEnumerable<PostPreviewViewModel> GetPostsModels(IEnumerable<Post> posts)
		{
			return posts.Select(GetPostPreviewViewModelFromPost);
		} 

		private PostPreviewViewModel GetPostPreviewViewModelFromPost(Post post)
		{
			return new PostPreviewViewModel(
				post.Title,
				post.PostSeoSetting.Url,
				post.Content,
				post.PublishDate.ToString("dd MMM yyyy", new CultureInfo("ru")),
				post.PostSettings.PreviewImage?.FilePath
			);
		}
	}
}