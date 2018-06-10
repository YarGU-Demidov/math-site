using System.Threading.Tasks;
using MathSite.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;

namespace MathSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeViewModelBuilder _modelBuilder;
        private readonly ISitemapProvider _sitemapProvider;

        public HomeController(
            IHomeViewModelBuilder modelBuilder,
            ISitemapProvider sitemapProvider
        )
        {
            _modelBuilder = modelBuilder;
            _sitemapProvider = sitemapProvider;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _modelBuilder.BuildIndexModel());
        }

        public async Task<IActionResult> SiteMap()
        {
            return _sitemapProvider.CreateSitemap(await _modelBuilder.GenerateSiteMap());
        }
    }
}