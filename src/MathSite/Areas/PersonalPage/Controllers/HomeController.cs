using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.PersonalPage.Controllers
{
	[Area("personal-page")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}