using System;
using System.Threading.Tasks;
using MathSite.Common.ActionResults;
using MathSite.Common.Extensions;
using MathSite.Common.FileFormats;
using MathSite.Facades.FileSystem;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    [Route("file")]
    public class FileController : Controller
    {
        private readonly IFileFacade _fileFacade;
        private readonly FileFormatBuilder _fileFormatBuilder;

        public FileController(IFileFacade fileFacade, FileFormatBuilder fileFormatBuilder)
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

            return new FileStreamInlineResult(fileStream, fileFormat.ContentType) {FileDownloadName = fileName};
        }

        [Route("download/{id:guid}")]
        public async Task<IActionResult> Download(Guid id)
        {
            var (fileName, fileStream, extension) = await _fileFacade.GetFileAsync(id);
            
            if(fileStream.IsNull())
                return NotFound();

            var fileFormat = _fileFormatBuilder.GetFileFormatForExtension(extension);
            return File(fileStream, fileFormat.ContentType, fileName);
        }
    }
}