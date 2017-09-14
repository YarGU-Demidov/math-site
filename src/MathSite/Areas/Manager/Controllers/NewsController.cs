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
	public class NewsController : BaseController
	{
		private readonly ISiteSettingsFacade _siteSettingsFacade;

		public NewsController(IUserValidationFacade userValidationFacade, ISiteSettingsFacade siteSettingsFacade) : base(userValidationFacade)
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
					new MenuLink("Dashboard", "/manager", false),
					new MenuLink("Статьи", "/manager", false),
					new MenuLink("Новости", "/manager/news", true),
					new MenuLink("Файлы", "/manager", false),
					new MenuLink("Пользователи", "/manager", false),
					new MenuLink("Настройки", "/manager", false),
				})
			));
		}
	}
}