using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class DotxFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
        public override IEnumerable<string> Extensions { get; } = new[] {".dotx"};
    }
}