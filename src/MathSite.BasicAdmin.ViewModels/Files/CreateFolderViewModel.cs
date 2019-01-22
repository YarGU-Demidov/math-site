using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;

namespace MathSite.BasicAdmin.ViewModels.Files
{
    public class CreateFolderViewModel : AdminPageBaseViewModel
    {
        public string FolderName { get; set; }
        public string Path { get; set; }
    }
}