using System.Threading.Tasks;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHomeViewModelBuilder _modelBuilder;

        public HomeController(IUserValidationFacade userValidationFacade, IHomeViewModelBuilder modelBuilder)
            : base(userValidationFacade)
        {
            _modelBuilder = modelBuilder;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _modelBuilder.BuildIndexModel());
        }
    }
}