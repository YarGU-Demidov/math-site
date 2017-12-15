using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;

namespace MathSite.BasicAdmin.ViewModels.Files
{
    public class DirectoryViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CreationDate { get; set; }
        public string FullPath { get; set; }
    }

    public class FileViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CreationDate { get; set; }
        public string FullFilePath { get; set; }
    }

    public class IndexFilesViewModel : AdminPageBaseViewModel
    {
        public IEnumerable<DirectoryViewModel> Directories { get; set; }
        public IEnumerable<FileViewModel> Files { get; set; }
    }
}