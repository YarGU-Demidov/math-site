using System.Collections.Generic;
using MathSite.Controllers;
using MathSite.Db;
using MathSite.ViewModels.Api.MenuItems;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	public class MenuItemsController : BaseController
	{
		public MenuItemsController(MathSiteDbContext dbContext) : base(dbContext)
		{
		}

		[HttpGet]
		public IEnumerable<MenuItemData> GetAll()
		{
			var home = new MenuItemData("Главная", "/", "home");
			var usersManagement = new MenuItemData("Пользователи", "/users", "face", new[]
			{
				new MenuItemData("Группы", "/groups"),
				new MenuItemData("Права", "/groups-rights")
			});

			var siteSettingsManagement = new MenuItemData("Настройки", "/site-settings", "settings");

			return new[]
			{
				home,
				usersManagement,
				siteSettingsManagement
			};
		}
	}
}