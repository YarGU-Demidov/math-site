using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Images
{
    public class WebpFileFormat : BaseFileFormat
    {
        public override string ContentType { get; } = "image/webp";
        public override IEnumerable<string> Extensions { get; } = new []{ ".webp" };
        public override FileType FileType { get; } = FileType.Image;
    }
}