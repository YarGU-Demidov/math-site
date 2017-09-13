using System.Collections.Generic;
using MathSite.Controllers;
using MathSite.Db;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Api.MenuItems;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	public class MenuItemsController : BaseController
	{
		public MenuItemsController(IUserValidationFacade userValidationFacade) : base(userValidationFacade)
		{
		}

		[HttpGet]
		public IEnumerable<MenuItemData> GetAll()
		{
			var home = new MenuItemData("Главная", "/", "home");

			var personsManagement = new MenuItemData("Персоны", "/persons", "person", new[]
			{
				new MenuItemData("Список", "/persons/list"),
				new MenuItemData("Добавить", "/persons/add")
			});

			var usersManagement = new MenuItemData("Пользователи", "/users", "face", new[]
			{
				new MenuItemData("Список", "/users/list"),
				new MenuItemData("Добавить", "/users/add")
			});

			var groupsManagement = new MenuItemData("Группы", "/groups", "group", new[]
			{
				new MenuItemData("Список", "/groups/list"),
				new MenuItemData("Добавить", "/groups/add")
			});

			var siteSettingsManagement = new MenuItemData("Настройки", "/site-settings", "settings");


			return new[]
			{
				home,
				personsManagement,
				groupsManagement,
				usersManagement,
				siteSettingsManagement
			};
		}

	}
}