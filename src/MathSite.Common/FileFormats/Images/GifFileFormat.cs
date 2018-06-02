using System.Collections.Generic;
using System.Net.Mime;

namespace MathSite.Common.FileFormats.Images
{
    public class GifFileFormat : BaseFileFormat
    {
        public override string ContentType { get; } = "image/gif";
        public override IEnumerable<string> Extensions { get; } = new[] {".gif"};
        public override FileType FileType { get; } = FileType.Image;
    }
}