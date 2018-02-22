using System;
using System.ComponentModel.DataAnnotations;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;

namespace MathSite.BasicAdmin.ViewModels.Persons
{
    public class BasePersonEditViewModel : AdminPageBaseViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public DateTime? BirthDate {get; set; }
        public string Phone { get; set; }
    }
}