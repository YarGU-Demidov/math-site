using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class DocxFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public override IEnumerable<string> Extensions { get; } = new[] {".docx"};
    }
}