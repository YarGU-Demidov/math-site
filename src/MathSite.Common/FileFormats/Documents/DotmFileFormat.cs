using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class DotmFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/vnd.ms-word.template.macroEnabled.12";
        public override IEnumerable<string> Extensions { get; } = new[] {".dotm"};
    }
}