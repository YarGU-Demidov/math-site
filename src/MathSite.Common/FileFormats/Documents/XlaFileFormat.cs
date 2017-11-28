using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class XlaFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/vnd.ms-excel";
        public override IEnumerable<string> Extensions { get; } = new[] {".xla"};
    }
}