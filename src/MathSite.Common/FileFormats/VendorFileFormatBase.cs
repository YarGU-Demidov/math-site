namespace MathSite.Common.FileFormats
{
    public abstract class VendorFileFormatBase : BaseFileFormat
    {
        public override FileType FileType { get; } = FileType.Vendor;
    }
}