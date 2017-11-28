using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class DocFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/msword";
        public override IEnumerable<string> Extensions { get; } = new[] {".doc"};
    }
}