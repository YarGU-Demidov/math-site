using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class PpamFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/vnd.ms-powerpoint.addin.macroEnabled.12";

        public override IEnumerable<string> Extensions { get; } = new[] {".ppam"};
    }
}