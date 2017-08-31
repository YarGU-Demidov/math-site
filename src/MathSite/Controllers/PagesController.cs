using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class PagesController : Controller
	{
		public IActionResult Index(string query)
		{
			return View("Index", query);
		}
	}
}