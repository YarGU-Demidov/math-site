using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Controllers;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
	[Area("manager"), Authorize("admin")]
	public class HomeController : BaseController
	{
		private readonly ISiteSettingsFacade _siteSettingsFacade;

		public HomeController(IUserValidationFacade userValidationFacade, ISiteSettingsFacade siteSettingsFacade) : base(userValidationFacade)
		{
			_siteSettingsFacade = siteSettingsFacade;
		}

		public async Task<IActionResult> Index()
		{
			return View(new AdminPageBaseViewModel(
				new PageTitleViewModel(
					"Dashboard",
					await _siteSettingsFacade[SiteSettingsNames.TitleDelimiter],
					await _siteSettingsFacade[SiteSettingsNames.SiteName]
				),
				new TopMenuViewModel(new List<MenuLink>
				{
					new MenuLink("Dashboard", "/", true),
					new MenuLink("Статьи", "/", false),
					new MenuLink("Новости", "/", false),
					new MenuLink("Файлы", "/", false),
					new MenuLink("Пользователи", "/", false),
					new MenuLink("Настройки", "/", false),
				})
			));
		}
	}
}