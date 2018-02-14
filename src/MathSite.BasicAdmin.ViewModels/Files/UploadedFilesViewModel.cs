using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;

namespace MathSite.BasicAdmin.ViewModels.Files
{
    public class UploadedFilesViewModel : AdminPageBaseViewModel
    {
        public string FileName { get; set; }
        public string FileId { get; set; }
    }
}