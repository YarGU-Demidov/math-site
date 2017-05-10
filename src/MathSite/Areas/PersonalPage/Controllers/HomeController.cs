using MathSite.Controllers;
using MathSite.Db;
using MathSite.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.PersonalPage.Controllers
{
	[Area("personal-page")]
	[Authorize("peronal-page")]
	public class HomeController : BaseController
	{
		public HomeController(MathSiteDbContext dbContext, IBusinessLogicManger logicManger) : base(dbContext, logicManger)
		{
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}