using System.ComponentModel.DataAnnotations;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;

namespace MathSite.BasicAdmin.ViewModels.Settings
{
    public class IndexSettingsViewModel : AdminPageWithPagingViewModel
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