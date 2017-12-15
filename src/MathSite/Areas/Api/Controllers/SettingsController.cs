using MathSite.Controllers;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
    [Area("Api")]
    public class SettingsController : BaseController
    {
        public SettingsController(IUserValidationFacade userValidationFacade, IUsersFacade usersFacade) 
            : base(userValidationFacade, usersFacade)
        {
        }
    }
}