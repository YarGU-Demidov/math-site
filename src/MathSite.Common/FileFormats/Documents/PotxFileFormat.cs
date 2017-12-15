using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class PotxFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } =
            "application/vnd.openxmlformats-officedocument.presentationml.template";

        public override IEnumerable<string> Extensions { get; } = new[] {".potx"};
    }
}