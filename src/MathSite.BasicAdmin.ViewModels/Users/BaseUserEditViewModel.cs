using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;

namespace MathSite.BasicAdmin.ViewModels.Users
{
    public class BaseUserEditViewModel : AdminPageBaseViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfimation { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string GroupId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string PersonId { get; set; }

        public IEnumerable<(string Id, string Name)> Groups { get; set; }
        public IEnumerable<(string Id, string Name)> Persons { get; set; }
    }
}