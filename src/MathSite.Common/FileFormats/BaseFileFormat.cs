using System.Collections.Generic;

namespace MathSite.Common.FileFormats
{
    public abstract class BaseFileFormat : IFileFormat
    {
        public virtual string ContentType { get; } = "application/octet-stream";
        public abstract IEnumerable<string> Extensions { get; }
        public abstract FileType FileType { get; }
        public virtual string ImageUrl { get; } = "";
    }
}