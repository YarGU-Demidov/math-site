using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Images
{
    public class PngFileFormat : BaseFileFormat
    {
        public override string ContentType { get; } = "image/png";
        public override IEnumerable<string> Extensions { get; } = new []{ ".png" };
        public override FileType FileType { get; } = FileType.Image;
    }
}