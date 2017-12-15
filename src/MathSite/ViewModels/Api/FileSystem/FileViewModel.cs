using MathSite.Common.FileFormats;
using MathSite.Entities;

namespace MathSite.ViewModels.Api.FileSystem
{
    public class FileViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string DirectoryId { get; set; }
        public string Icon { get; set; }

        public static FileViewModel FromData(File file, FileFormatBuilder formatBuilder)
        {
            var format = formatBuilder.GetFileFormatForExtension(file.Extension);
            return new FileViewModel
            {
                Name = file.Name,
                Id = file.Id.ToString(),
                DirectoryId = file.DirectoryId.ToString(),
                Extension = file.Extension,
                Icon = string.IsNullOrWhiteSpace(format.ImageUrl) ? $"/uploads/icons/files/{format.FileType.ToString().ToLowerInvariant()}.svg" : format.ImageUrl
            };
        }
    }
}