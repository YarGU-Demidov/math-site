using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Categories;
using MathSite.Controllers;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Authorize(RightAliases.AdminAccess)]
    [Area("manager")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesViewModelBuilder _modelBuilder;

        public CategoriesController(
            IUserValidationFacade userValidationFacade, 
            IUsersFacade usersFacade,
            ICategoriesViewModelBuilder modelBuilder
        ) : base(userValidationFacade, usersFacade)
        {
            _modelBuilder = modelBuilder;
        }

        [Route("[area]/[controller]/")]
        [Route("[area]/[controller]/index")]
        [Route("[area]/[controller]/list")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View("Index", await _modelBuilder.BuildIndexViewModelAsync(page, perPage));
        }
    }
}