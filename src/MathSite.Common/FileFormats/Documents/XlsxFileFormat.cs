using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Documents
{
    public class XlsxFileFormat : VendorFileFormatBase
    {
        public override string ContentType { get; } =
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public override IEnumerable<string> Extensions { get; } = new[] {".xlsx"};
    }
}