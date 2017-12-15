using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.Extensions;
using MathSite.Common.FileFormats;
using MathSite.Controllers;
using MathSite.Core.Responses;
using MathSite.Core.Responses.ResponseTypes;
using MathSite.Facades.FileSystem;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Api.FileSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
    public class UploadFormViewModel
    {
        public IEnumerable<IFormFile> Files { get; set; }
    }

    [Produces("application/json")]
    [Route("api/fs")]
    [Authorize("admin")]
    public class FileSystemController : BaseController
    {
        private readonly IDirectoryFacade _directoryFacade;
        private readonly IFileFacade _fileFacade;
        private readonly FileFormatBuilder _formatBuilder;

        public FileSystemController(
            IUserValidationFacade userValidationFacade, 
            IDirectoryFacade directoryFacade,
            IFileFacade fileFacade, 
            FileFormatBuilder formatBuilder, 
            IUsersFacade usersFacade
        )
            : base(userValidationFacade, usersFacade)
        {
            _directoryFacade = directoryFacade;
            _fileFacade = fileFacade;
            _formatBuilder = formatBuilder;
        }

        [Route("get-root-dir")]
        public async Task<DirectoryViewModel> GetRootDirectory()
        {
            return DirectoryViewModel.FromData(await _directoryFacade.GetDirectoryWithPathAsync("/"), _formatBuilder);
        }

        [Route("download/{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            try
            {
                var file = await _fileFacade.GetFileAsync(id);

                if (file.IsNull())
                    return NotFound();

                var format = _formatBuilder.GetFileFormatForExtension(Path.GetExtension(file.FileName));
                return FileInline(file.FileStream, format.ContentType, file.FileName);
            }
            catch (DirectoryNotFoundException)
            {
                return NotFound();
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("upload")]
        [DisableRequestSizeLimit]
        public async Task<BaseResponse<string>> Upload(UploadFormViewModel model)
        {
            if (model.Files == null || model.Files.All(file => file.Length <= 0))
                return new BaseResponse<string>(new ErrorResponseType(), "No files");

            var fileIds = new List<Guid>();

            foreach (var file in model.Files)
            {
                if (file.Length <= 0)
                    continue;

                fileIds.Add(await _fileFacade.SaveFileAsync(CurrentUser, file.FileName, file.OpenReadStream()));
            }

            var result =
                $"{model.Files?.Sum(f => f.Length) / (1024D * 1024D):N}MB for files: {model.Files.Select(info => info.Name).Aggregate((f, s) => $"{f}, {s}")}. " +
                $"Files ids are: {fileIds.Select(g => g.ToString()).Aggregate((f, s) => $"{f}, {s}")}";

            GC.Collect();

            return new BaseResponse<string>(
                new SuccessResponseType(),
                null,
                result
            );
        }
    }
}