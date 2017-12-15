using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class PotmFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/vnd.ms-powerpoint.template.macroEnabled.12";

        public override IEnumerable<string> Extensions { get; } = new[] {".potm"};
    }
}