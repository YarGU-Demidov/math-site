using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using MathSite.Facades.SiteSettings;

namespace MathSite.ViewModels.SharedModels.SecondaryPage
{
	public abstract class SecondaryViewModelBuilder : CommonViewModelBuilder
	{
		protected SecondaryViewModelBuilder(ISiteSettingsFacade siteSettingsFacade) 
			: base(siteSettingsFacade)
		{
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
			model.Featured = new List<PostPreviewViewModel>
			{
				new PostPreviewViewModel("Test1", "/news/test1-url", "Test content for 1st post", DateTime.Now.ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test2", "/news/test2-url", "Test content for 2nd post", DateTime.Now.AddMonths(-2).AddDays(-1).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test3", "/news/test3-url", "Test content for 3d post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru")))
			};
		}
	}
}