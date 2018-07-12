using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Facades.Conversations;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Controllers
{
    [Authorize]
    public class MessagingController:BaseController
    {
        public MessagingController(IUserValidationFacade userValidationFacade, IUsersFacade usersFacade)
          : base(userValidationFacade, usersFacade)
        {
        }

        public IActionResult Chat()
        {       
            return View();
        }

    }
}
