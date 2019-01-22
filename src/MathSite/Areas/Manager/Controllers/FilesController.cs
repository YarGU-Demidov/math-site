using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Files;
using MathSite.Common.Exceptions;
using MathSite.Common.Extensions;
using MathSite.Controllers;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.FileSystem;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Authorize(RightAliases.AdminAccess)]
    [Area("manager")]
    [Route("[area]/[controller]")]
    public class FilesController : BaseController
    {
        private readonly IFileFacade _fileFacade;
        private readonly IFilesManagerViewModelBuilder _filesManagerViewModelBuilder;

        public FilesController(
            IUserValidationFacade userValidationFacade, 
            IUsersFacade usersFacade,
            IFileFacade fileFacade,
            IFilesManagerViewModelBuilder filesManagerViewModelBuilder
        ) : base(userValidationFacade, usersFacade)
        {
            _fileFacade = fileFacade;
            _filesManagerViewModelBuilder = filesManagerViewModelBuilder;
        }

        [Route("")]
        [Route("list")]
        [Route("index")]
        public async Task<IActionResult> Index([FromQuery] string path = "/")
        {
            return View(await _filesManagerViewModelBuilder.BuildIndexViewModelAsync(path));
        }

        [Route("create-folder")]
        [HttpGet]
        public async Task<IActionResult> CreateFolder(string path = "/")
        {
            return View("CreateFolder", await _filesManagerViewModelBuilder.BuildCreateFolderViewModelAsync(path));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create-folder")]
        public async Task<IActionResult> CreateFolder(CreateFolderViewModel model)
        {
            try
            {
                await _filesManagerViewModelBuilder.CreateFolderViewModelAsync(model.Path, model.FolderName);
            }
            catch (ArgumentOutOfRangeException)
            {
                ViewData.Add("error", "Такая папка уже существует, придумайте другое имя");
                var folderName = model.FolderName;
                model = await _filesManagerViewModelBuilder.BuildCreateFolderViewModelAsync(model.Path);
                model.FolderName = folderName;
                return View("CreateFolder", model);
            }

            return RedirectToAction("Index", new { path = model.Path });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("upload")]
        public async Task<IActionResult> UploadFile(List<IFormFile> files, [FromQuery] string path = "/")
        {
            if (files.IsNullOrEmpty())
                return BadRequest("You forget to append file.");

            var filesData = files.Select(formFile => (formFile.FileName, formFile.OpenReadStream()));

            return View("Uploaded", await _filesManagerViewModelBuilder.BuildUploadedViewModelAsync(CurrentUser, filesData, path));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid id, string path = "/")
        {
            try
            {
                await _fileFacade.Remove(id);
                return RedirectToActionPermanent("Index", new {path});
            }
            catch (FileIsUsedException)
            {
                return BadRequest("Файл используется, удалить нельзя!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete-folder/{id:guid}")]
        public async Task<IActionResult> DeleteFolder(Guid id, string currentPath)
        {
            await _filesManagerViewModelBuilder.DeleteFolderAsync(id);
            return RedirectToAction("Index", new {path = currentPath});
        }

        [HttpPost("UploadBase64Image")]
        public async Task<IActionResult> UploadBase64Image(string base64Image, string pageType)
        {
            var dataString = base64Image.Contains(",")
                ? base64Image.Split(',', StringSplitOptions.RemoveEmptyEntries)
                : null;

            if (pageType.IsNullOrWhiteSpace())
            {
                return BadRequest("Wrong page type!");
            }

            if (dataString.IsNull() || dataString?.Length != 2)
            {
                return BadRequest(base64Image);
            }

            var data = Convert.FromBase64String(dataString[1]);
            var fileId = await _filesManagerViewModelBuilder.BuildUploadBase64Image(CurrentUser, data, pageType);

            return Json(fileId);
        }
    }
}