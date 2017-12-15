using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class XlamFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/vnd.ms-excel.addin.macroEnabled.12";

        public override IEnumerable<string> Extensions { get; } = new[] {".xlam"};
    }
}