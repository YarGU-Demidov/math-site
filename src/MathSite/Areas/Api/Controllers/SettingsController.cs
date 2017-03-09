using MathSite.Controllers;
using MathSite.Core;
using MathSite.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	public class SettingsController: BaseController
	{
		public SettingsController(IMathSiteDbContext dbContext) : base(dbContext)
		{
		}

		public IActionResult GetLogoutUrl()
		{
			var logoutUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Api/Auth/Logout";

			return Json(logoutUrl);
		}
	}
}