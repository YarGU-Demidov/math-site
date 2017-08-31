using MathSite.Db;
using MathSite.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class ErrorsController : BaseController
	{
		public ErrorsController(MathSiteDbContext dbContext, IBusinessLogicManger logicManger) : base(dbContext, logicManger)
		{
		}

		[Route("/error/404")]
		public IActionResult NotFound404()
		{
			Response.StatusCode = 404;
			return View();
		}

		[AllowAnonymous]
		[Route("/error/403")]
		public IActionResult Forbidden(string returnUrl)
		{
			Response.StatusCode = 403;
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}
	}
}