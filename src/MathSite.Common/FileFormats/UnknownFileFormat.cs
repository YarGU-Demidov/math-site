using System.Collections.Generic;

namespace MathSite.Common.FileFormats
{
    public class UnknownFileFormat : BaseFileFormat
    {
        public override IEnumerable<string> Extensions { get; } = new List<string>();
        public override FileType FileType { get; } = FileType.Unknown;
    }
}