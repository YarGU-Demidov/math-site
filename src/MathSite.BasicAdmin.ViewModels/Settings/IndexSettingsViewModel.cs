using System.ComponentModel.DataAnnotations;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;

namespace MathSite.BasicAdmin.ViewModels.Settings
{
    public class IndexSettingsViewModel : AdminPageBaseViewModel
    {
        [Required]
        public string SiteName { get; set; }
        public string DefaultTitleForNewsPage { get; set; }
        public string DefaultTitleForHomePage { get; set; }
        [Required]
        public int PerPageCount { get; set; }
        [Required]
        public string TitleDelimiter { get; set; }
    }
}