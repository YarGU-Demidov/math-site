using MathSite.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class ErrorsController : BaseController
	{
		public ErrorsController(IMathSiteDbContext dbContext) : base(dbContext)
		{
		}

		[Route("/errors/404")]
		public IActionResult NotFound404()
		{
			Response.StatusCode = 404;
			return View();
		}

		[AllowAnonymous]
		[Route("/errors/403")]
		public IActionResult Forbidden(string returnUrl)
		{
			Response.StatusCode = 403;
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}
	}
}