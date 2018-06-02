using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class PpaFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/vnd.ms-powerpoint";
        public override IEnumerable<string> Extensions { get; } = new[] {".ppa"};
    }
}