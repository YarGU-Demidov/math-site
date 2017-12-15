using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Files;
using MathSite.Controllers;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Authorize("admin")]
    [Area("manager")]
    public class FilesController : BaseController
    {
        private readonly IFilesManagerViewModelBuilder _filesManagerViewModelBuilder;

        public FilesController(
            IUserValidationFacade userValidationFacade, 
            IUsersFacade usersFacade,
            IFilesManagerViewModelBuilder filesManagerViewModelBuilder
        ) : base(userValidationFacade, usersFacade)
        {
            _filesManagerViewModelBuilder = filesManagerViewModelBuilder;
        }

        [Route("manager/files")]
        [Route("manager/files/list")]
        [Route("manager/files/index")]
        public async Task<IActionResult> Index([FromQuery] string path = "/")
        {
            return View(await _filesManagerViewModelBuilder.BuildIndexViewModelAsync(path));
        }
    }
}