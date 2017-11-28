using System.Collections.Generic;

namespace MathSite.Common.FileFormats.Images
{
    public class JpgFileFormat : BaseFileFormat
    {
        public override string ContentType { get; } = "image/jpeg";
        public override IEnumerable<string> Extensions { get; } = new []{ ".jpg", ".jpeg" };
        public override FileType FileType { get; } = FileType.Image;
    }
}