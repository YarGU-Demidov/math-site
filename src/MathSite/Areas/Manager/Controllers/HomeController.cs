using MathSite.Controllers;
using MathSite.Db;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
	[Authorize("admin")]
	[Area("manager")]
	public class HomeController : BaseController
	{
		public HomeController(IUserValidationFacade userValidationFacade) : base(userValidationFacade)
		{
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}