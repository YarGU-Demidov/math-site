using MathSite.Common.FileFormats;
using Xunit;

namespace MathSite.Tests.Common
{
    public class FileFormatBuilderTests
    {
        private FileFormatBuilder GetBuilder()
        {
            return new FileFormatBuilder();
        }

        [Fact]
        public void GetExistingFormatTest()
        {
            var builder = GetBuilder();

            var ff1 = builder.GetFileFormatForExtension(".jpg");
            var ff2 = builder.GetFileFormatForExtension(".jpeg");
            var ff3 = builder.GetFileFormatForExtension(".png");

            Assert.NotNull(ff1);
            Assert.NotNull(ff2);
            Assert.NotNull(ff3);

            Assert.True(ff1 == ff2);

            Assert.Equal(FileType.Image, ff1.FileType);
            Assert.Equal(FileType.Image, ff2.FileType);
            Assert.Equal(FileType.Image, ff3.FileType);

            Assert.Equal(new[] {".jpg", ".jpeg"}, ff1.Extensions);
            Assert.Equal(new[] {".jpg", ".jpeg"}, ff2.Extensions);
            Assert.Equal(new[] {".png"}, ff3.Extensions);
        }

        [Fact]
        public void GetNonexistentFormatTest()
        {
            var builder = GetBuilder();

            var ff = builder.GetFileFormatForExtension("test file format which is nonexistent");
            
            Assert.NotNull(ff);
            
            Assert.Equal(FileType.Unknown, ff.FileType);
            Assert.Empty(ff.Extensions);
        }
    }
}