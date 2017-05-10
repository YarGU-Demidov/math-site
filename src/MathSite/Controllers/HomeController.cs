using System;
using System.Linq;
using MathSite.Db;
using MathSite.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class HomeController : BaseController
	{

		public HomeController(MathSiteDbContext dbContext, IBusinessLogicManger businessLogicManager) : base(dbContext, businessLogicManager)
		{
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}