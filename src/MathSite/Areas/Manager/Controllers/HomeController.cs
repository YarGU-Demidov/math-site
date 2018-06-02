using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Home;
using MathSite.Controllers;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Area("manager")]
    [Authorize(RightAliases.AdminAccess)]
    public class HomeController : BaseController
    {
        private readonly IDashboardPageViewModelBuilder _modelBuilder;

        public HomeController(IUserValidationFacade userValidationFacade, IDashboardPageViewModelBuilder modelBuilder, IUsersFacade usersFacade) :
            base(userValidationFacade, usersFacade)
        {
            _modelBuilder = modelBuilder;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _modelBuilder.BuilDashboardPageViewModel());
        }
    }
}