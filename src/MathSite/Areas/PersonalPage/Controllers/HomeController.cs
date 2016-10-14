using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.PersonalPage.Controllers
{
	[Area("personal-page")]
	[Authorize("peronal-page")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}