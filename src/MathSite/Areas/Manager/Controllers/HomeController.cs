using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Home;
using MathSite.Controllers;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Area("manager")]
    [Authorize("admin")]
    public class HomeController : BaseController
    {
        private readonly IDashboardPageViewModelBuilder _modelBuilder;

        public HomeController(IUserValidationFacade userValidationFacade, IDashboardPageViewModelBuilder modelBuilder) :
            base(userValidationFacade)
        {
            _modelBuilder = modelBuilder;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _modelBuilder.BuilDashboardPageViewModel());
        }
    }
}