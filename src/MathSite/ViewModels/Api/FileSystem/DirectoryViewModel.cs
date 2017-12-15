using System.Collections.Generic;
using System.Linq;
using MathSite.Common.FileFormats;
using MathSite.Entities;

namespace MathSite.ViewModels.Api.FileSystem
{
    public class DirectoryViewModel
    {
        private static string IconUrl { get; } = "/uploads/icons/directory.svg";

        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<DirectoryViewModel> Directories { get; set; } = new List<DirectoryViewModel>();
        public ICollection<FileViewModel> Files { get; set; } = new List<FileViewModel>();
        public string Icon { get; set; } = IconUrl;

        public static DirectoryViewModel FromData(Directory directory, FileFormatBuilder formatBuilder)
        {
            return new DirectoryViewModel
            {
                Directories = directory.Directories.Select(d => FromData(d, formatBuilder)).ToList(),
                Files = directory.Files.Select(f => FileViewModel.FromData(f, formatBuilder)).ToList(),
                Id = directory.Id.ToString(),
                Name = directory.Name
            };
        }
    }
}