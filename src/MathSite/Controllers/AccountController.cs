using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IUserValidationFacade userValidationFacade, IUsersFacade usersFacade)
            : base(userValidationFacade, usersFacade)
        {
        }

        [HttpGet("/login")]
        public IActionResult Login(string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
                returnUrl = "/";

            if (HttpContext.User.Identity.IsAuthenticated)
                if (!string.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");

            return View(new LoginFormViewModel {ReturnUrl = returnUrl});
        }

        [HttpPost("/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginFormViewModel model)
        {
            var valid = TryValidateModel(model);

            if (!valid)
                return View(model);

            var ourUser = await UserValidationFacade.GetUserByLoginAndPasswordAsync(model.Login, model.Password);

            if (ourUser == null)
                return View(model);

            if (ourUser.TwoFactorAutentificationHash == null)
            {
                ViewBag.HasTwoFactorAutentification = false;
                return View(model);
            }
            ViewBag.HasTwoFactorAutentification = true;
            return View(model);
        }

        public async Task<IActionResult> CheckLogin(string login)
        {
            return await UsersFacade.DoesUserExistsAsync(login)
                ? Json(true)
                : Json("Данного пользователя не существует");
        }

        public async Task<IActionResult> CheckPassword(string password, string login)
        {
            var user = await UserValidationFacade.GetUserByLoginAndPasswordAsync(login, password);

            return user != null
                ? Json(true)
                : Json("Пароль неверен");
        }

        [Authorize("logout")]
        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("/continuelogin")]
        public async Task<IActionResult> ContinueLogin(LoginFormViewModel model)
        {
            var ourUser = await UserValidationFacade.GetUserByLoginAndPasswordAsync(model.Login, model.Password);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new List<Claim>
                        {
                            new Claim("UserId", ourUser.Id.ToString())
                        },
                        "Auth"
                    )
                ),
                new AuthenticationProperties {ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(12)}
            );

            var returnUrl = HttpContext.Request.Query["returnUrl"].ToString();

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost("/Add2FA")]
        public async void Add2Fa(LoginFormViewModel model)
        {
            //some code to add two factor autorization
            await ContinueLogin(model);
        }
    }
}