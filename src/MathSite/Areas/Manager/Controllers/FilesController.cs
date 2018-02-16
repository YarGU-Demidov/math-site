using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Files;
using MathSite.Common.Extensions;
using MathSite.Controllers;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Route("[area]/[controller]")]
        [Route("[area]/[controller]/list")]
        [Route("[area]/[controller]/index")]
        public async Task<IActionResult> Index([FromQuery] string path = "/")
        {
            return View(await _filesManagerViewModelBuilder.BuildIndexViewModelAsync(path));
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromQuery] string path = "/")
        {
            if (file.IsNull())
                return BadRequest("You forget to append file.");

            return View("Uploaded", await _filesManagerViewModelBuilder.BuildUploadedViewModelAsync(CurrentUser, file.FileName, file.OpenReadStream(), path));
        }
    }
}