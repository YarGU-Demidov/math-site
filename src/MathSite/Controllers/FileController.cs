using System;
using System.Threading.Tasks;
using MathSite.Common.Extensions;
using MathSite.Common.FileFormats;
using MathSite.Facades.FileSystem;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    [Route("file")]
    public class FileController : BaseController
    {
        private readonly IFileFacade _fileFacade;
        private readonly FileFormatBuilder _fileFormatBuilder;

        public FileController(IUserValidationFacade userValidationFacade, IUsersFacade usersFacade,
             IFileFacade fileFacade, FileFormatBuilder fileFormatBuilder)
            : base(userValidationFacade, usersFacade)
        {
            _fileFacade = fileFacade;
            _fileFormatBuilder = fileFormatBuilder;
        }

        [Route("get/{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var (fileName, fileStream, extension) = await _fileFacade.GetFileAsync(id);

            if(fileStream.IsNull())
                return NotFound();

            var fileFormat = _fileFormatBuilder.GetFileFormatForExtension(extension);
            return FileInline(fileStream, fileFormat.ContentType, fileName);
        }

        [Route("download/{id:guid}")]
        public async Task<IActionResult> Download(Guid id)
        {
            var (fileName, fileStream, extension) = await _fileFacade.GetFileAsync(id);
            var fileFormat = _fileFormatBuilder.GetFileFormatForExtension(extension);
            return File(fileStream, fileFormat.ContentType, fileName);
        }
    }
}