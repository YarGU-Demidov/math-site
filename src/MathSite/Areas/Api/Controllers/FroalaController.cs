using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.Extensions;
using MathSite.Controllers;
using MathSite.Facades.FileSystem;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
    [Authorize("admin")]
    [Area("api")]
    public class FroalaController : BaseController
    {
        private readonly IFileFacade _fileFacade;

        public FroalaController(
            IUserValidationFacade userValidationFacade,
            IUsersFacade usersFacade,
            IFileFacade fileFacade
        ) : base(userValidationFacade, usersFacade)
        {
            _fileFacade = fileFacade;
        }

        [HttpGet("[area]/[controller]/get-images")]
        public async Task<IActionResult> GetImages()
        {
            var extensions = new[] {".png", ".jpg", ".jpeg", ".gif"};
            var files = await _fileFacade.GetFilesByExtensions(extensions);
            return Json(files.Select(file => new {id = file.Id, url = Url.Action("Get", "File", new {id = file.Id})}));
        }

        [HttpPost("[area]/[controller]/upload-file/{dirName?}")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, string dirName = null)
        {
            if (file.IsNull())
                return Json(new {error = "You didn't sent a file."});

            var fileId = await _fileFacade.SaveFileAsync(CurrentUser, file.FileName, file.OpenReadStream(), dirName);

            return Json(new
            {
                link = Url.Action("Get", "File", new {id = fileId})
            });
        }

        [HttpPost("[area]/[controller]/delete-image")]
        public async Task<IActionResult> DeleteImage([FromForm] Guid id)
        {
            await _fileFacade.Remove(id);
            return Json(true);
        }
    }
}