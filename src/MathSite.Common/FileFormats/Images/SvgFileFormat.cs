using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Images
{
    public class SvgFileFormat : BaseFileFormat
    {
        public override string ContentType { get; } = "image/svg+xml";
        public override IEnumerable<string> Extensions { get; } = new []{ ".svg" };
        public override FileType FileType { get; } = FileType.Image;
    }
}