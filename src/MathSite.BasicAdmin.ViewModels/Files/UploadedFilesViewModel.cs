using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;

namespace MathSite.BasicAdmin.ViewModels.Files
{
    public class UploadedFilesViewModel : AdminPageBaseViewModel
    {
        public IEnumerable<(string Name, string Id)> Files { get; set; }
    }
}