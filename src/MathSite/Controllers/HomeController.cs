using System.Threading.Tasks;
using MathSite.Db;
using MathSite.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IHomeViewModelBuilder _modelBuilder;

		public HomeController(MathSiteDbContext dbContext, IHomeViewModelBuilder modelBuilder)
			: base(dbContext)
		{
			_modelBuilder = modelBuilder;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _modelBuilder.BuildIndexModel());
		}
	}
}