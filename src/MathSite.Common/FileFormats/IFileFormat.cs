using System.Collections.Generic;

namespace MathSite.Common.FileFormats
{
    public interface IFileFormat
    {
        string ContentType { get; }
        IEnumerable<string> Extensions { get; }
        FileType FileType { get; }
        string ImageUrl { get; }
    }
}