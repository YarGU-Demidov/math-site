using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class XltxFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } =
            "application/vnd.openxmlformats-officedocument.spreadsheetml.template";

        public override IEnumerable<string> Extensions { get; } = new[] {".xltx"};
    }
}