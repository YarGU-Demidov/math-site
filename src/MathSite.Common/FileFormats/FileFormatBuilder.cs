using System.Collections.Generic;
using System.Linq;
using MathSite.Common.FileFormats.Documents;
using MathSite.Common.FileFormats.Images;

namespace MathSite.Common.FileFormats
{
    public class FileFormatBuilder
    {
        private static readonly UnknownFileFormat UnknownFileFormat = new UnknownFileFormat();
        private readonly ICollection<IFileFormat> _fileFormats = new List<IFileFormat>();

        public FileFormatBuilder()
        {
            AddImages();
            AddOfficeDocuments();
        }

        private void AddOfficeDocuments()
        {
            // word files
            _fileFormats.Add(new DocFileFormat());
            _fileFormats.Add(new DocxFileFormat());
            _fileFormats.Add(new DotFileFormat());
            _fileFormats.Add(new DotxFileFormat());
            _fileFormats.Add(new DocmFileFormat());
            _fileFormats.Add(new DotmFileFormat());

            // excel files
            _fileFormats.Add(new XlsFileFormat());
            _fileFormats.Add(new XltFileFormat());
            _fileFormats.Add(new XltxFileFormat());
            _fileFormats.Add(new XltmFileFormat());
            _fileFormats.Add(new XlaFileFormat());
            _fileFormats.Add(new XlamFileFormat());
            _fileFormats.Add(new XlsbFileFormat());
            _fileFormats.Add(new XlsmFileFormat());
            _fileFormats.Add(new XlsxFileFormat());

            // powerpoint
            _fileFormats.Add(new PptFileFormat());
            _fileFormats.Add(new PotFileFormat());
            _fileFormats.Add(new PpsFileFormat());
            _fileFormats.Add(new PpaFileFormat());
            _fileFormats.Add(new PptxFileFormat());
            _fileFormats.Add(new PotxFileFormat());
            _fileFormats.Add(new PpsxFileFormat());
            _fileFormats.Add(new PpamFileFormat());
            _fileFormats.Add(new PptmFileFormat());
            _fileFormats.Add(new PotmFileFormat());
            _fileFormats.Add(new PpsmFileFormat());

            // access
            _fileFormats.Add(new MdbFileFormat());
        }

        private void AddImages()
        {
            _fileFormats.Add(new GifFileFormat());
            _fileFormats.Add(new JpgFileFormat());
            _fileFormats.Add(new PngFileFormat());
            _fileFormats.Add(new SvgFileFormat());
            _fileFormats.Add(new WebpFileFormat());
        }

        public IFileFormat GetFileFormatForExtension(string extension)
        {
            return _fileFormats.FirstOrDefault(format => format.Extensions.Any(formatExt => formatExt == extension)) ??
                   UnknownFileFormat;
        }
    }
}