using MathSite.Db;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class BaseController : Controller
	{
		protected readonly IMathSiteDbContext DbContext;

		public BaseController(IMathSiteDbContext dbContext)
		{
			DbContext = dbContext;
		}
	}
}