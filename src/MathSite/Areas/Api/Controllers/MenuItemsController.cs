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
		public MenuItemsController(IMathSiteDbContext dbContext) : base(dbContext)
		{
		}

		[HttpGet]
		public IEnumerable<MenuItemData> GetAll()
		{
			var usersManagement = new MenuItemData("Пользователи", "/users", "face", new[]
			{
				new MenuItemData("Группы", "/users/groups/"),
				new MenuItemData("Права", "/users/groups-rights")
			});

			var siteSettingsManagement = new MenuItemData("Настройки", "/site-settings", "settings");

			return new[]
			{
				usersManagement,
				siteSettingsManagement
			};
		}
	}
}