using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class PptmFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/vnd.ms-powerpoint.presentation.macroEnabled.12";

        public override IEnumerable<string> Extensions { get; } = new[] {".pptm"};
    }
}