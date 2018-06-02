using System;

namespace MathSite.BasicAdmin.ViewModels.Users
{
    public class EditUsersViewModel : BaseUserEditViewModel
    {
        public string Id { get; set; }
        public bool ResetPassword { get; set; }
    }
}