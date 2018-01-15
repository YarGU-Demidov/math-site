using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    public class EventsController : BaseController
    {
        public EventsController(IUserValidationFacade userValidationFacade, IUsersFacade usersFacade) : base(
            userValidationFacade, usersFacade)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}