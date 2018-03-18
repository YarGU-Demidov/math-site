using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.ViewModels.Account
{
    public class LoginFormViewModel : CommonViewModel
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Необходим логин")]
        [Remote(controller: "Account", action: "CheckLogin")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Необходим пароль")]
        [Remote(controller: "Account", action: "CheckPassword", AdditionalFields = "Login")]
        public string Password { get; set; }
        public string BarcodeImageUrl { get; set; }
        public string SetupCode { get; set; }
        public string TemporalTwoFactorAutenticationKey { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public string Token { get; set; }
        public string IsTokenCorrect { get; set; }
    }
}