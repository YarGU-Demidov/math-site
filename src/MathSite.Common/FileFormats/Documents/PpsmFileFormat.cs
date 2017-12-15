using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class PpsmFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } = "application/vnd.ms-powerpoint.slideshow.macroEnabled.12";

        public override IEnumerable<string> Extensions { get; } = new[] {".ppsm"};
    }
}