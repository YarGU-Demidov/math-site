using System;
using System.Linq;
using MathSite.Db;
using MathSite.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Controllers
{
	public class HomeController : BaseController
	{
		private IBusinessLogicManger Logic { get; }

		public HomeController(MathSiteDbContext dbContext, IBusinessLogicManger businessLogicManager) : base(dbContext,
			businessLogicManager)
		{
			Logic = businessLogicManager;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}