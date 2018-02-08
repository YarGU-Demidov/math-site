using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.ActionResults;
using MathSite.Common.Extensions;
using MathSite.Entities;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MathSite.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController(IUserValidationFacade userValidationFacade, IUsersFacade usersFacade)
        {
            UserValidationFacade = userValidationFacade;
            UsersFacade = usersFacade;
        }

        protected IUserValidationFacade UserValidationFacade { get; }
        protected IUsersFacade UsersFacade { get; }

        protected Guid? CurrentUserId => CurrentUser?.Id;
        protected User CurrentUser { get; private set; }

        [NonAction]
        private async Task TrySetUser(ActionContext context)
        {

            if (!context.HttpContext.User.Identity.IsAuthenticated)
                return;

            var userId = context.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == "UserId")
                ?.Value;

            CurrentUser = await UsersFacade.GetCurrentUserAsync(userId);

            if (CurrentUser.IsNull())
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        protected FileContentInlineResult FileInline(byte[] fileContents, string contentType, string fileDownloadName)
        {
            return new FileContentInlineResult(fileContents, contentType) {FileDownloadName = fileDownloadName};
        }

        protected FileStreamInlineResult FileInline(Stream fileStream, string contentType, string fileDownloadName)
        {
            return new FileStreamInlineResult(fileStream, contentType) {FileDownloadName = fileDownloadName};
        }

        [NonAction]
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await TrySetUser(context);
            await next();
        }
    }
}