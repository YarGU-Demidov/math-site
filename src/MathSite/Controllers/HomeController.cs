using System.Threading.Tasks;
using MathSite.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeViewModelBuilder _modelBuilder;

        public HomeController(
            IHomeViewModelBuilder modelBuilder
        )
        {
            _modelBuilder = modelBuilder;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _modelBuilder.BuildIndexModel());
        }

        public IActionResult SiteMap()
        {
            // TODO: REWRITE THIS!!!
            return NotFound();
            // return Content(_modelBuilder.GenerateSiteMap(), "text/xml", Encoding.UTF8);
        }
    }
}