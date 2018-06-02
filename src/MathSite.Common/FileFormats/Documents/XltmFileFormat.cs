using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class XltmFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/vnd.ms-excel.template.macroEnabled.12";

        public override IEnumerable<string> Extensions { get; } = new[] {".xltm"};
    }
}